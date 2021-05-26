using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject fightText;

    public List<GameObject> spawnList;

    private bool hasGameStarted = false;

    public SoundHandler soundHandler;

    void Start()
    {
        soundHandler = GetComponent<SoundHandler>();
        StartGame();
    }

    void SpawnPlayer(int index, Vector3 spawnPosition)
    {
        GameObject.Find("PlayerConfiguration-" + index).GetComponent<PlayerInstanciationController>().handleInstanciate(index, spawnPosition);
    }

    void SpawnPlayer(int index)
    {
        SpawnPlayer(index, GetAvailablePositionToSpawn());
    }

    void StartGame()
    {
        StartCoroutine(HandleStartGame());
    }

    IEnumerator HandleStartGame()
    {
        GameEvents.current.StartMusicTrigger("game");
        GameEvents.current.ChangeInputSchemeTrigger("Player");
        fightText.SetActive(true);
        soundHandler.ChangeTheSound(0);
        yield return new WaitForSeconds(2.0f);
        fightText.SetActive(false);
        soundHandler.ChangeTheSound(1);
        for (int i = 0; i < GameInfo.numberOfPlayers; i++)
        {
            SpawnPlayer(i, spawnList[i].transform.position);
        }
        yield return new WaitForSeconds(0.5f);
        hasGameStarted = true;
    }

    Vector3 GetAvailablePositionToSpawn()
    {
        int spawnIndex = Random.Range(0, 3);
        print("spawn index " + spawnIndex);
        while (!spawnList[spawnIndex].GetComponent<SpawnController>().isAvailableForSpawnPlayer)
        {
            spawnIndex = Random.Range(0, 3);
        }
        return spawnList[spawnIndex].transform.position;
    }

    void GoToScore()
    {
        StartCoroutine(HandleGoToScore());
    }

    IEnumerator HandleGoToScore()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1;
        GameInfo.sceneToLoad = "4-ScoreMenu";
        SceneManager.LoadScene("LoadingSceneWithTransition");
    }

    void Update()
    {
        if (hasGameStarted)
        {
            for (int i = 0; i < GameInfo.numberOfPlayers; i++)
            {
                if (GameInfo.playerScores[i] >= GameInfo.maxScore)
                {
                    GoToScore();
                }
                if (!GameObject.Find("Player-" + (i + 1)))
                {
                    SpawnPlayer(i);
                }
            }
        }
    }

}
