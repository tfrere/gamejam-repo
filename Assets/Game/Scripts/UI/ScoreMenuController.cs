using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreMenuController : MonoBehaviour
{
    public TextMeshPro winText;
    public List<TextMeshPro> playerTextScores;
    public List<GameObject> playerPanes;
    public GameObject paneHolder;
    public string targetScene;

    void Start()
    {
        GameEvents.current.StartMusicTrigger("menu");

        for(int i = 0; i < GameInfo.numberOfPlayers; i++) {
            if(GameInfo.playerScores[i] == GameInfo.maxScore) {
                winText.SetText("Player " + (i + 1) + " won");
                playerPanes[i].transform.position += new Vector3(0,0.2f,0);
            }
            playerTextScores[i].text = GameInfo.playerScores[i] + "";
        }

        for(int i = GameInfo.numberOfPlayers; i < 4; i++) {
            playerPanes[i].SetActive(false);
        }

        if(GameInfo.numberOfPlayers == 2) {
            paneHolder.transform.position += new Vector3(3.6f,0,0);
        }
        if(GameInfo.numberOfPlayers == 3) {
            paneHolder.transform.position += new Vector3(1.75f,0,0);
        }
    }

    void Update()
    {
        if(Input.GetKey("space")) {
            GameInfo.sceneToLoad = targetScene;
            SceneManager.LoadScene("LoadingSceneWithTransition");
        }
    }
}
