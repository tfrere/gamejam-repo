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
        SceneManager.LoadSceneAsync(LoadingData.sceneToLoad);
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
        "confessions de minuit\nle vin fait maison\ncolore nos dents",
        "Journée de l’an neuf –\nles hommes que nous croisons\nregardent ma fille",
        "Spectacle en plein air\nsur la ligne d'horizon\nl'astre funambule"
      };

      return haikus[Random.Range(0, 3)];
    }
}
