using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject PlayerOne;
    private string playerOneName;
    public GameObject PlayerTwo;
    private string playerTwoName;

    public GameObject PlayerOneSpawn;
    public GameObject PlayerTwoSpawn;

    public TextMeshPro PlayerOneScoreText;
    public TextMeshPro PlayerTwoScoreText;

    public GameObject RoundNumberTextGameObject;
    private TextMeshPro RoundNumberTextMeshPro;
    private SoundHandler soundHandler;

    private bool isRoundActive = false;

    private int roundNumber = 0;

    void Start()
    {
        RoundNumberTextMeshPro = RoundNumberTextGameObject.GetComponent<TextMeshPro>();
        soundHandler = GetComponent<SoundHandler>();
        // Reset scores
        GameInfo.PlayerOneScore = 0;
        GameInfo.PlayerTwoScore = 0;
        playerOneName = PlayerOne.name;
        playerTwoName = PlayerTwo.name;
        FirstStartRound();
    }

    void Update()
    {
        PlayerOneScoreText.SetText("" + GameInfo.PlayerOneScore);
        PlayerTwoScoreText.SetText("" + GameInfo.PlayerTwoScore);
        RoundNumberTextMeshPro.SetText("Round " + roundNumber);

        if(GameInfo.PlayerOneScore == GameInfo.MaxScore || GameInfo.PlayerTwoScore == GameInfo.MaxScore) {
            Time.timeScale = 1;
            GoToScore();
        }

        if (isRoundActive == true && (GameObject.Find("PlayerOne") == null || GameObject.Find("PlayerTwo") == null)) {
            StartRound();
        }

    }

    void goToScoreMenu() {
        GameInfo.sceneToLoad = "ScoreMenu";
        SceneManager.LoadScene("LoadingSceneWithTransition");
    }

    void GoToScore() {
        StartCoroutine(HandleGoToScore());
    }

    void StartRound() {
        StartCoroutine(HandleStartRound());
    }

    void FirstStartRound() {
        StartCoroutine(HandleFirstStartRound());
    }

    void PopPlayers() {
        GameObject playerOne = Instantiate(PlayerOne, PlayerOneSpawn.transform.position, Quaternion.identity);
        playerOne.name = playerOneName;
        GameObject playerTwo = Instantiate(PlayerTwo, PlayerTwoSpawn.transform.position, Quaternion.identity);
        playerTwo.name = playerTwoName;
    }


    IEnumerator HandleGoToScore()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(1f);
        GameInfo.sceneToLoad = "ScoreMenu";
        SceneManager.LoadScene("LoadingSceneWithTransition");
    }

    IEnumerator HandleFirstStartRound()
    {
        // print("firstRound");
        isRoundActive = false; 
        RoundNumberTextGameObject.SetActive(true);
        soundHandler.ChangeTheSound(0);
        yield return new WaitForSeconds(2.0f);
        RoundNumberTextGameObject.SetActive(false);
        soundHandler.ChangeTheSound(1);
        PopPlayers();
        yield return new WaitForSeconds(0.5f);
        isRoundActive = true;
        roundNumber++;
    }
    IEnumerator HandleStartRound()
    {
        // print("Round");
        isRoundActive = false;
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 1;
        if(GameObject.Find("PlayerOne") == null) {
            Destroy(GameObject.Find("PlayerTwo"));
        }
        if(GameObject.Find("PlayerTwo") == null) {
            Destroy(GameObject.Find("PlayerOne"));
        }
        RoundNumberTextGameObject.SetActive(true);
        soundHandler.ChangeTheSound(0);
        yield return new WaitForSeconds(2.0f);
        RoundNumberTextGameObject.SetActive(false);
        soundHandler.ChangeTheSound(1);
        PopPlayers();
        yield return new WaitForSeconds(1.0f);
        isRoundActive = true;
        roundNumber++;
    }

}
