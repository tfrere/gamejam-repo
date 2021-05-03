using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;

    public GameObject Player1Spawn;
    public GameObject Player2Spawn;

    public TextMeshPro PlayerOneScoreText;
    public TextMeshPro PlayerTwoScoreText;

    public GameObject RoundNumberTextGameObject;
    private TextMeshPro RoundNumberTextMeshPro;
    private SoundHandler soundHandler;

    private bool isRoundActive = false;
    private bool isFirstRound = true;

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
        RoundNumberTextMeshPro.SetText("Round " + (GameInfo.PlayerOneScore + GameInfo.PlayerTwoScore));

        if(GameInfo.PlayerOneScore == GameInfo.MaxScore || GameInfo.PlayerTwoScore == GameInfo.MaxScore) {
            goToScoreMenu();
        }

        if (isRoundActive == true && (GameObject.Find("Player(Clone)") == null || GameObject.Find("Player2(Clone)") == null)) {
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
        Instantiate(Player1, Player1Spawn.transform.position, Quaternion.identity);
        Instantiate(Player2, Player2Spawn.transform.position, Quaternion.identity);
    }

    IEnumerator HandleFirstStartRound()
    {
        print("firstRound");
        isRoundActive = false; 
        RoundNumberTextGameObject.SetActive(true);
        soundHandler.ChangeTheSound(0);
        yield return new WaitForSeconds(4.0f);
        RoundNumberTextGameObject.SetActive(false);
        soundHandler.ChangeTheSound(1);
        PopPlayers();
        yield return new WaitForSeconds(1.0f);
        soundHandler.ChangeTheSound(2);
        isRoundActive = true;
    }
    IEnumerator HandleStartRound()
    {
        print("Round");
        isRoundActive = false;
        soundHandler.ChangeTheSound(2);
        yield return new WaitForSeconds(2.0f);
        if(GameObject.Find("Player(Clone)") == null) {
            Destroy(GameObject.Find("Player2(Clone)"));
        }
        if(GameObject.Find("Player2(Clone)") == null) {
            Destroy(GameObject.Find("Player(Clone)"));
        }
        RoundNumberTextGameObject.SetActive(true);
        soundHandler.ChangeTheSound(0);
        yield return new WaitForSeconds(4.0f);
        RoundNumberTextGameObject.SetActive(false);
        soundHandler.ChangeTheSound(1);
        PopPlayers();
        yield return new WaitForSeconds(1.0f);
        soundHandler.ChangeTheSound(2);
        isRoundActive = true;
        isFirstRound = false;
    }

}
