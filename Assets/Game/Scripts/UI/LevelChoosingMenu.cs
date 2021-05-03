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
        isActive = true;
        if(Input.GetKey("space")) {
            GameInfo.sceneToLoad = name;
            SceneManager.LoadScene("LoadingScene");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isActive = false;
    }

    void Update()
    {
        if(isActive) {
            level.transform.position = new Vector3(level.transform.position.x, -2.5f, level.transform.position.z);
        }
        else {
            level.transform.position = new Vector3(level.transform.position.x, -3.0f, level.transform.position.z);
        }
    }
}
