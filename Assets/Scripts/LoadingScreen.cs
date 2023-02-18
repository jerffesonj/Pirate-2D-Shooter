using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Class responsible to do the loading to the right scene
public class LoadingScreen : MonoBehaviour
{

    [SerializeField] float waitingTime;
    [SerializeField] public int sceneToLoadIndex;

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelEnum());
    }
    
    //Enumerator responsible to load the scene async on the loading screen
    IEnumerator LoadLevelEnum()
    {
        yield return new WaitForSeconds(waitingTime);
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync(sceneToLoadIndex);
        loadLevel.allowSceneActivation = false;

        while (!loadLevel.isDone)
        {
            yield return new WaitForSeconds(waitingTime);
            loadLevel.allowSceneActivation = true;
        }
    }
}
