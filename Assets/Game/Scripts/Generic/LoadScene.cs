using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using Random=UnityEngine.Random;

public class LoadScene : MonoBehaviour
{
    public float delay = 5.0f;
    public bool isCoroutineExecuting = false;
    public bool hasToBeInterrupted = false;
    public TextMeshPro textArea;
    public TeleType teletype;

    void Start()
    {
      textArea.SetText(getHaiku());
      StartCoroutine(ExecuteAfterTime(delay, () => {
        SceneManager.LoadSceneAsync(GameInfo.sceneToLoad);
      }));
    }

    void Update() {
      if(Input.GetKey("space")) {
        teletype.Interruption();
        StartCoroutine(InterruptAfterTime(1f, () => {
          SceneManager.LoadSceneAsync(GameInfo.sceneToLoad);
        }));
      }
    }

    // This part has to be entirely re-written :]

    IEnumerator InterruptAfterTime(float time, Action task)
    {
        if (hasToBeInterrupted)
            yield break;
        hasToBeInterrupted = true;
        yield return new WaitForSeconds(time);
        task();
        hasToBeInterrupted = false;
    }

    IEnumerator ExecuteAfterTime(float time, Action task)
    {
        if (isCoroutineExecuting)
            yield break;
        isCoroutineExecuting = true;
        yield return new WaitForSeconds(time);
        task();
        isCoroutineExecuting = false;
    }

    String getHaiku()
    {
      String[] haikus = {
        "Ground squirrel.\nbalancing its tomato.\non the garden fence...\n\n- Don Eulert",
        "From across the lake,\nPast the black winter trees,\nFaint sounds of a flute...\n\n- Richard Wright",
        "A little boy sings\non a terrace, eyes aglow.\nRidge spills upward...\n\n- Robert Yehling",
      };

      return haikus[Random.Range(0, 3)];
    }
}
