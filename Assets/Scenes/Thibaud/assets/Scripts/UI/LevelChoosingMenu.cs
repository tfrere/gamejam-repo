using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChoosingMenu : MonoBehaviour
{
    public GameObject level;
    private bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKey("space")) {
            print("OnTriggerStay2D - " + name);
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(name);
            float loadProgress = loadingOperation.progress;
            isActive = true;
            if(loadingOperation.isDone) {

            }
        }
        else {
            isActive = false;
        }
    }

    void Update()
    {
        if(isActive) {
            level.transform.y = 3;
        }
        else {
            level.transform.y = 2;
        }
    }
}
