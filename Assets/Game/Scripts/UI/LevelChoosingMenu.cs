using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChoosingMenu : MonoBehaviour
{
    public GameObject level;
    public string levelName;
    public GameObject levelStroke;
    private bool isActive;
    public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions
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
        float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

        if(isActive) {
            levelStroke.SetActive(true);
            // Vector3 test = new Vector3(level.transform.position.x, -2.5f, level.transform.position.z);
            // level.transform.position = Vector3.Lerp(level.transform.position, test, interpolationRatio);
            level.transform.position = new Vector3(level.transform.position.x, -2.5f, level.transform.position.z);
        }
        else {
            levelStroke.SetActive(false);
            // Vector3 test = new Vector3(level.transform.position.x, -3f, level.transform.position.z);
            // level.transform.position = Vector3.Lerp(level.transform.position, test, interpolationRatio);
            level.transform.position = new Vector3(level.transform.position.x, -3f, level.transform.position.z);
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

