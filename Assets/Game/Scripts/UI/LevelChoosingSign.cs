using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChoosingSign : MonoBehaviour
{
    public GameObject levelText;
    public string levelName;
    public GameObject levelStroke;
    private bool isActive;
    int elapsedFrames = 0;

    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isActive = true;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        isActive = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        StartEnableSign();
    }

    void Update()
    {

        if(isActive) {
            levelStroke.SetActive(true);
            levelText.transform.position = new Vector3(levelText.transform.position.x, -2.5f, levelText.transform.position.z);
        }
        else {
            levelStroke.SetActive(false);
            levelText.transform.position = new Vector3(levelText.transform.position.x, -3f, levelText.transform.position.z);
        }
       if(Input.GetKey("space") && isActive) {
           print(levelName);
            GameInfo.sceneToLoad = levelName;
            SceneManager.LoadScene("LoadingScene");
        }
    }

    void StartEnableSign()
    {
      StartCoroutine(EnableSign());
    }

    IEnumerator EnableSign()
    {
        yield return new WaitForSeconds(0.1f);
        isActive = false;
    }
}

