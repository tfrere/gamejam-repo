using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMenuController : MonoBehaviour
{

  public string targetScene;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKey("space")) {
        GameInfo.sceneToLoad = targetScene;
        SceneManager.LoadScene("LoadingSceneWithTransition");
      }
    }
}
