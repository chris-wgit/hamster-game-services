using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Utilities.Inspector;

public static class EventHandlerScene
{
    public static UnityAction<SceneField, LoadSceneMode> OnLoadSceneEvent;
    public static UnityAction<SceneField> OnUnLoadSceneEvent;

    public static void LoadScene(SceneField sceneToLoad, LoadSceneMode mode)
    {
        OnLoadSceneEvent?.Invoke(sceneToLoad, mode);
    }

    public static void UnLoadScene(SceneField sceneToLoad)
    {
        OnUnLoadSceneEvent?.Invoke(sceneToLoad);
    }
}
