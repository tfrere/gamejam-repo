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

  void Update()
  {
    if(Input.GetKey("space")) {
      GameInfo.sceneToLoad = targetScene;
      SceneManager.LoadScene("LoadingSceneWithTransition");
    }
  }
  
}
