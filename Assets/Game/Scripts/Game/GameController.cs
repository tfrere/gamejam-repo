using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject PlayerOne;
    public GameObject PlayerTwo;

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
        FirstStartRound();
    }

    void Update()
    {
        PlayerOneScoreText.SetText("" + GameInfo.PlayerOneScore);
        PlayerTwoScoreText.SetText("" + GameInfo.PlayerTwoScore);
        RoundNumberTextMeshPro.SetText("Round " + roundNumber);

        if(GameInfo.PlayerOneScore == GameInfo.MaxScore || GameInfo.PlayerTwoScore == GameInfo.MaxScore) {
            Time.timeScale = 1;
            goToScoreMenu();
        }

        if (isRoundActive == true && (GameObject.Find("PlayerOne(Clone)") == null || GameObject.Find("PlayerTwo(Clone)") == null)) {
            StartRound();
        }

    }

    void goToScoreMenu() {
        GameInfo.sceneToLoad = "ScoreMenu";
        SceneManager.LoadScene("LoadingSceneWithTransition");
    }

    void StartRound() {
        StartCoroutine(HandleStartRound());
    }

    void FirstStartRound() {
        StartCoroutine(HandleFirstStartRound());
    }

    void PopPlayers() {
        Instantiate(PlayerOne, PlayerOneSpawn.transform.position, Quaternion.identity);
        Instantiate(PlayerTwo, PlayerTwoSpawn.transform.position, Quaternion.identity);
    }

    IEnumerator HandleFirstStartRound()
    {
        // print("firstRound");
        isRoundActive = false; 
        RoundNumberTextGameObject.SetActive(true);
        soundHandler.ChangeTheSound(0);
        yield return new WaitForSeconds(4.0f);
        RoundNumberTextGameObject.SetActive(false);
        soundHandler.ChangeTheSound(1);
        PopPlayers();
        yield return new WaitForSeconds(1.0f);
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
        if(GameObject.Find("PlayerOne(Clone)") == null) {
            Destroy(GameObject.Find("PlayerTwo(Clone)"));
        }
        if(GameObject.Find("PlayerTwo(Clone)") == null) {
            Destroy(GameObject.Find("PlayerOne(Clone)"));
        }
        RoundNumberTextGameObject.SetActive(true);
        soundHandler.ChangeTheSound(0);
        yield return new WaitForSeconds(4.0f);
        RoundNumberTextGameObject.SetActive(false);
        soundHandler.ChangeTheSound(1);
        PopPlayers();
        yield return new WaitForSeconds(1.0f);
        isRoundActive = true;
        roundNumber++;
    }

}
