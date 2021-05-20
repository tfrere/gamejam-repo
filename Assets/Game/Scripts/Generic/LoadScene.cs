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
    public TextMeshPro textArea;

    void Start()
    {
      textArea.SetText(getHaiku());
      StartCoroutine(ExecuteAfterTime(delay, () => {
        SceneManager.LoadSceneAsync(GameInfo.sceneToLoad);
      }));
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
