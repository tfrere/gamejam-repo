using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenMenuController : MonoBehaviour
{

  public string targetScene;

  void Start()
  {
    // GameEvents.instance.OnGameLaunchTrigger(1);
  }

  void Update()
  {
    if(Input.GetKey("space")) {
      GameInfo.sceneToLoad = targetScene;
      SceneManager.LoadScene("LoadingSceneWithTransition");
    }
  }
  
}
