using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using Random=UnityEngine.Random;

public class LoadSceneWithTransition : MonoBehaviour
{
    public float delay = 5.0f;
    public bool isCoroutineExecuting = false;

    void Start()
    {
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
}
