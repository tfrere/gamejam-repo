using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenMenuController : MonoBehaviour
{
    public string targetScene;

    void Start()
    {
        GameEvents.current.StartMusicTrigger("menu");
    }
    void OnEnable()
    {
        GameEvents.current.OnUISubmit += LoadNextScene;
    }
    void OnDisable()
    {
        GameEvents.current.OnUISubmit -= LoadNextScene;
    }
    void LoadNextScene()
    {
        GameInfo.sceneToLoad = targetScene;
        StartCoroutine(HandleLoadNextScene());
    }

    IEnumerator HandleLoadNextScene()
    {
        GameEvents.current.UISoundTrigger("validate");
        yield return new WaitForSeconds(.3f);
        SceneManager.LoadScene("LoadingSceneWithTransition");
    }

}
