using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Inspector;
using UnityEngine.SceneManagement;
using System;

public class UIPauseController : MonoBehaviour
{
    public SceneField mainScene;

    public void LeaveGame()
    {
        StartCoroutine(CoDisconnect(() =>
        {
            EventHandlerScene.LoadScene(mainScene, LoadSceneMode.Single);
        }));

    }

    private IEnumerator CoDisconnect(Action onSuccess)
    {
        if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();

        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected) yield return null;

        onSuccess?.Invoke();
    }
}
