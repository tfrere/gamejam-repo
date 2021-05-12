using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenMenuController : MonoBehaviour
{

  public string targetScene;

  void Start()
  {
    SceneManager.LoadScene("BackgroundMusic", LoadSceneMode.Additive);
  }

  void Update()
  {
    if(Input.GetKey("space")) {
      GameInfo.sceneToLoad = targetScene;
      SceneManager.LoadScene("LoadingSceneWithTransition");
    }
  }
  
}
