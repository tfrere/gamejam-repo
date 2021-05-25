using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    public List<TextMeshPro> scoreTextList;

    public List<GameObject> scoreList;

    void Start()
    {
        GameInfo.resetScores();
        for(int i = GameInfo.numberOfPlayers; i < 4; i++) {
            scoreList[i].SetActive(false);
        }
    }

    void Update()
    {
        for(int i = 0; i < GameInfo.numberOfPlayers; i++) {
            scoreTextList[i].SetText("" + GameInfo.playerScores[i]);
        }
    }

}
