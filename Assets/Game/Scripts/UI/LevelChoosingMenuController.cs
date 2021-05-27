using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class LevelChoosingMenuController : MonoBehaviour
{
    public List<LevelConfig> levelList;
    public int currentLevelIndex = 0;
    public SpriteRenderer currentLevelSpriteRenderer;
    private bool isChangingLevel = false;

    void Start()
    {
        StartCoroutine(HandleChangeInputScheme());
        StartCoroutine(UpdateCurrentLevel());
    }

    void OnEnable()
    {
        GameEvents.current.OnUINavigation += ChangeLevel;
        GameEvents.current.OnUISubmit += SubmitLevel;
    }
    void OnDisable()
    {
        GameEvents.current.OnUINavigation -= ChangeLevel;
        GameEvents.current.OnUISubmit -= SubmitLevel;
    }

    void ChangeLevel(string orientation)
    {
        print("have to update currernt level " + orientation);
        if (!isChangingLevel)
        {
            if (orientation == "left")
            {
                currentLevelIndex--;
                if (currentLevelIndex < 0)
                {
                    currentLevelIndex = levelList.Count - 1;
                }
                StartCoroutine(UpdateCurrentLevel());
            }
            if (orientation == "right")
            {
                currentLevelIndex++;
                if (currentLevelIndex > levelList.Count - 1)
                {
                    currentLevelIndex = 0;
                }
                StartCoroutine(UpdateCurrentLevel());
            }
        }
    }

    IEnumerator UpdateCurrentLevel()
    {
        isChangingLevel = true;
        currentLevelSpriteRenderer.sprite = levelList[currentLevelIndex].levelImage;
        yield return new WaitForSeconds(.1f);
        isChangingLevel = false;
    }

    IEnumerator HandleChangeInputScheme()
    {
        yield return new WaitForSeconds(.1f);
        GameEvents.current.ChangeInputSchemeTrigger("UI");
    }

    void SubmitLevel()
    {
        GameInfo.sceneToLoad = levelList[currentLevelIndex].levelSceneName;
        SceneManager.LoadScene("LoadingScene");
    }

}