using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utilities.Inspector;

[CreateAssetMenu(menuName = "Events/LoadScene Event Channel")]
public class LoadSceneEventSO : EventChannelBaseSO
{
    public SceneField sceneToLoad;
    public LoadSceneMode loadMode;

    public void RaiseEvent()
    {
        EventHandlerScene.LoadScene(sceneToLoad, loadMode);
    }
}
