using UnityEngine;
using Utilities.Inspector;
using UnityEngine.SceneManagement;

public class LoadSceneBehaviour : MonoBehaviour
{

    private void OnEnable()
    {
        EventHandlerScene.OnLoadSceneEvent += LoadScene;
        EventHandlerScene.OnUnLoadSceneEvent += UnLoadScene;
    }

    private void OnDisable()
    {
        EventHandlerScene.OnLoadSceneEvent -= LoadScene;
        EventHandlerScene.OnUnLoadSceneEvent -= UnLoadScene;
    }

    private void UnLoadScene(SceneField sceneToUnload)
    {
        if(sceneToUnload != null)
        SceneManager.UnloadSceneAsync(sceneToUnload.SceneName);
    }

    private void LoadScene(SceneField sceneToLoad, LoadSceneMode mode)
    {
        SceneManager.LoadSceneAsync(sceneToLoad, mode);
    }

}
