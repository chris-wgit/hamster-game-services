using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utilities.Inspector;

[CreateAssetMenu(menuName = "Events/UnLoad Scene Event")]
public class UnLoadSceneEventSO : EventChannelBaseSO
{
    public SceneField sceneToLoad;
    public void RaiseEvent()
    {
        EventHandlerScene.UnLoadScene(sceneToLoad);
    }
}
