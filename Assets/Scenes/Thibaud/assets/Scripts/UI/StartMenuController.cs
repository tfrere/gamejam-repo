using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("space")) {
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync("LevelChoosingMenu");
            float loadProgress = loadingOperation.progress;
            if(loadingOperation.isDone)
            {
                print("toto");
            }
        }
    }
}
