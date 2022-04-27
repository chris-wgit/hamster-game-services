using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class CharacterStateSync : MonoBehaviour
{
    private Character character;
    private PhotonView photonView;

    private void Awake()
    {
        character = GetComponent<Character>();
        photonView = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        character.MovementState.OnStateChange += OnMovementStateChanged;
        character.ConditionState.OnStateChange += OnCharacterStateChanged;
    }
    private void OnDisable()
    {
        character.MovementState.OnStateChange -= OnMovementStateChanged;
        character.ConditionState.OnStateChange -= OnCharacterStateChanged;
    }

    private void OnCharacterStateChanged(CharacterConditions state)
    {
        if (photonView.IsMine)
            photonView.RPC("SetCondition", RpcTarget.OthersBuffered, (byte)state);
    }

    [PunRPC]
    public void SetCondition(byte stateID)
    {
        character.ConditionState.ChangeState((CharacterConditions)Enum.ToObject(typeof(CharacterConditions), stateID));
    }

    private void OnMovementStateChanged(MovementStates state)
    {
        if(photonView.IsMine)
            photonView.RPC("SetMovement", RpcTarget.OthersBuffered, (byte)state);
    }

    [PunRPC]
    public void SetMovement(byte stateID)
    {
        character.MovementState.ChangeState((MovementStates)Enum.ToObject(typeof(MovementStates), stateID));
    }
}
