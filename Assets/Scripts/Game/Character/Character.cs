using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviourPun
{
    //Character state machine
    public StateMachine<MovementStates> MovementState;
    public StateMachine<CharacterConditions> ConditionState;

    [ShowInInspector]
    public string movementState { get { return MovementState != null ? MovementState.CurrentState.ToString() : " "; } }
    [ShowInInspector]
    public string conditionState { get { return ConditionState != null ? ConditionState.CurrentState.ToString() : " "; } }

    [Required]
    public CharacterDataSO data;

    public GameStatisticSO gameStatistics;

    private InputController _inputController;

    [HideInInspector]
    public CharacterEquipWeapon characterEquip;

    private CharacterAbility[] characterAbilities;

    public HUDController hUDController;

    public string NickName { get; private set; }
    public int Team { get; private set; }
    public int Index { get; private set; }

    private string weaponID;
    
    [HideInInspector] public WeaponDataSO weaponData;

    public ISkillAbility[] skillAbilities;

    public Action OnCharacterReset;
    public event Action<CharacterType> OnCharacterInitialized; //Is this character is mine

    private CharacterController _controller;

    [HideInInspector]
    public Transform normalizedTransform;

    #region Initialization

    private void Awake()
    {
        PreInitialization();
    }
    protected virtual void PreInitialization()
    {
        //Initialize state machines
        MovementState = new StateMachine<MovementStates>(gameObject, true);
        ConditionState = new StateMachine<CharacterConditions>(gameObject, true);

        //Store our components for further use
        _inputController = GetComponent<InputController>();

        ConditionState.ChangeState(CharacterConditions.Normal);
        MovementState.ChangeState(MovementStates.Idle);

        characterAbilities = GetComponents<CharacterAbility>();
        _controller = GetComponent<CharacterController>();
        characterEquip = GetComponentInChildren<CharacterEquipWeapon>();

        skillAbilities = GetComponents<ISkillAbility>();

        if (gameStatistics == null) gameStatistics = Resources.Load<GameStatisticSO>("GameData/Game Statistic");
    }
    private void OnEnable()
    {
        ConditionState.OnStateChange += OnCharacterStateChanged;
    }
    private void OnDisable()
    {
        ConditionState.OnStateChange -= OnCharacterStateChanged;

    }
    private void Start()
    {
        CharacterOnlineInit();      
    }

    private void CharacterOnlineInit()
    {
        PhotonInitialization();
        SetupCharacterEquipment();
        if (photonView.IsMine)
        {
            normalizedTransform = SpawnBaseManager.instance.GetSpawnPosition(Team, Index);

            OnCharacterInitialized?.Invoke(CharacterType.Master);
            gameStatistics.SetMasterCharacter(gameObject);
            foreach (CharacterAbility ability in characterAbilities)
            {
                ability.Initialization();
            }
            ResetCharacterStates();
        }
        else
        {
            _inputController.enabled = false;
            StartCoroutine(CouInitOtherCharacter());
        }

    }

    private IEnumerator CouInitOtherCharacter()
    {
        yield return new WaitForSeconds(0.2f);
        while(gameStatistics.MasterCharacter == null)
            yield return null;
        InitOtherCharacter();
    }

    private void InitOtherCharacter()
    {
        if (Team == gameStatistics.masterTeam)
        {
            OnCharacterInitialized?.Invoke(CharacterType.Ally);
        }
        else
        {
            OnCharacterInitialized?.Invoke(CharacterType.Enemy);
        }
    }
    private void PhotonInitialization()
    {
        NickName = GetView().GetName();
        SetTeam(GetView().GetTeam());
        //weaponID = GetView().GetWeapon();
        weaponID = data.weaponData.id;
        Index = GetView().GetIndex();
    }

    void SetTeam(int index)
    {
        Team = GetView().GetTeam();
        if(Team != gameStatistics.masterTeam) AddObjectToCachedTargetObject();
    }
    private void SetupCharacterEquipment()
    {
        weaponData = characterEquip.weaponLibrary.GetWeapon(weaponID);
        characterEquip.SetWeapon(weaponID);
        if (photonView.IsMine) data.EquipWeapon(weaponID);
        for (int i = 0; i < skillAbilities.Length; i++)
        {
            skillAbilities[i].InitCharacterSkill();
        }
        if (PhotonNetwork.IsMasterClient)
        {
            //GameplayController.instance.PoolingProjectiles(weaponData.weaponSkill);
        }
    }
    #endregion
    private void OnCharacterStateChanged(CharacterConditions state)
    {
        switch (state)
        {
            case CharacterConditions.Invulrnerable:
                ResetCharacter();
                break;
            case CharacterConditions.Normal:
                break;
            case CharacterConditions.Disabled:
                break;
            case CharacterConditions.Dead:
                DeactiveCharacter();
                break;
        }
    }

    public void MasterCharacterDead(Player killer)
    {
        _controller.enabled = false;

        ConditionState.ChangeState(CharacterConditions.Dead);

        EventHandlerGameplay.SetMasterCharacterDead(killer);

        transform.DOMove(SpawnBaseManager.instance.GetSpawnPosition(Team, PhotonNetwork.LocalPlayer.GetIndex()).position, 3);
    }

    public void ResetCharacterStates()
    {
        _inputController.enabled = true;

        transform.rotation = Quaternion.LookRotation(normalizedTransform.forward, Vector3.up);
        MovementState.ChangeState(MovementStates.Idle);
        ConditionState.ChangeState(CharacterConditions.Invulrnerable);
        StartCoroutine(CoInvictableState());

        _controller.enabled = true;
    }

    private IEnumerator CoInvictableState()
    {
        yield return new WaitForSeconds(4);
        ConditionState.ChangeState(CharacterConditions.Normal);
    }

    protected virtual void ResetCharacter()
    {
        OnCharacterReset?.Invoke();

        if(IsTargetableObject()) AddObjectToCachedTargetObject();
    }

    protected virtual void DeactiveCharacter()
    {
        if(data.OnDespawnFX != null)
        {
            PoolManager.Spawn(data.OnDespawnFX, transform.position, Quaternion.identity);
        }

        RemoveObjectFromCachedTargetObject();
    }


    public bool CanSendRPC()
    {
        bool canSendRPC = false;
        if (photonView.IsMine) canSendRPC = true;
        return canSendRPC;
    }

    public void AddObjectToCachedTargetObject()
    {
        if (!gameStatistics.cachedTargetPlayer.Contains(gameObject))
            gameStatistics.cachedTargetPlayer.Add(gameObject);
    }

    public void RemoveObjectFromCachedTargetObject()
    {
        if (gameStatistics.cachedTargetPlayer.Contains(gameObject))
            gameStatistics.cachedTargetPlayer.Remove(gameObject);
    }

    public bool IsTargetableObject()
    {
        bool isTargetable = false;
        if (Team != gameStatistics.masterTeam) isTargetable = true;
        return isTargetable;
    }

    public PhotonView GetView()
    {
        return this.photonView;
    }

    
}



