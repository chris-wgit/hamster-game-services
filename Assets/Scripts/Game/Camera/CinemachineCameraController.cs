using Cinemachine;
using UnityEngine;

public class CinemachineCameraController : MonoBehaviour
{
    protected CinemachineVirtualCamera _virtualCamera;

    private bool isFollowing = false;

    public GameStatisticSO gameStatistic;

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _virtualCamera.enabled = false;
    }

    private void OnEnable()
    {
        gameStatistic.OnMasterCharacterSet += StartFollowing;
    }

    private void OnDisable()
    {
        gameStatistic.OnMasterCharacterSet -= StartFollowing;
    }


    public void SetCameraRotation(Transform targetTransform)
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetTransform.eulerAngles.y, transform.eulerAngles.z);
    }

    public void StartFollowing()
    {
        if (isFollowing) return;
        SetCameraRotation(gameStatistic.MasterCharacter.transform);
        isFollowing = true;
        _virtualCamera.Follow = gameStatistic.MasterCharacter.transform;
        _virtualCamera.enabled = true;
    }
}