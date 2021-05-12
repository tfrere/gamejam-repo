using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TestController : MonoBehaviour
{
    public GameObject fightText;

    public List<GameObject> spawnList;

    public List<TextMeshPro> scoreTextList;

    public List<TextMeshPro> arrowTextList;

    private bool hasGameStarted = false;

    public SoundHandler soundHandler;

    void Start()
    {
        GameInfo.PlayerOneScore = 0;
        GameInfo.PlayerTwoScore = 0;
        soundHandler = GetComponent<SoundHandler>();
        StartGame();
 }

    void SpawnPlayer(int index, Vector3 spawnPosition) {
        GameObject.Find("PlayerConfiguration-" + index).GetComponent<PlayerInstanciationController>().handleInstanciate(index, spawnPosition);
    }


    void SpawnPlayer(int index) {
        GameObject.Find("PlayerConfiguration-" + index).GetComponent<PlayerInstanciationController>().handleInstanciate(index, GetAvailablePositionToSpawn());
    }


    void StartGame() {
        StartCoroutine(HandleStartGame());
    }

    IEnumerator HandleStartGame()
    {
        fightText.SetActive(true);
        soundHandler.ChangeTheSound(0);
        yield return new WaitForSeconds(2.0f);
        fightText.SetActive(false);
        soundHandler.ChangeTheSound(1);
        SpawnPlayer(0, spawnList[0].transform.position);
        SpawnPlayer(1, spawnList[1].transform.position);
        yield return new WaitForSeconds(0.5f);
        GameInfo.GameState = "game";
        hasGameStarted = true;
    }

    Vector3 GetAvailablePositionToSpawn() {
        int spawnIndex = Random.Range(0, 3);
        while(!spawnList[spawnIndex].GetComponent<SpawnController>().isAvailableForSpawnPlayer) {
            spawnIndex = Random.Range(0, 3);
        }
        return spawnList[spawnIndex].transform.position;
    }

    void GoToScore() {
        StartCoroutine(HandleGoToScore());
    }

    IEnumerator HandleGoToScore()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1;
        GameInfo.sceneToLoad = "ScoreMenu";
        SceneManager.LoadScene("LoadingSceneWithTransition");
    }

    void Update()
    {
        scoreTextList[0].SetText("" + GameInfo.PlayerOneScore);
        scoreTextList[1].SetText("" + GameInfo.PlayerTwoScore);

        arrowTextList[0].SetText("" + GameInfo.PlayerOneArrows);
        arrowTextList[1].SetText("" + GameInfo.PlayerTwoArrows);

        if(hasGameStarted) {
            if(GameInfo.PlayerOneScore >= GameInfo.MaxScore || GameInfo.PlayerTwoScore >= GameInfo.MaxScore) {
                GoToScore();
            }

            if (GameObject.Find("PlayerOne") == null) {
                soundHandler.ChangeTheSound(1);
                SpawnPlayer(0);
            }
            if (GameObject.Find("PlayerTwo") == null) {
                soundHandler.ChangeTheSound(1);
                SpawnPlayer(1);
            }
        }

    }

}
