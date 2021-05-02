using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;

    public TextMeshPro PlayerOneScoreText;
    public TextMeshPro PlayerTwoScoreText;

    // Start is called before the first frame update
    void Start()
    {
        // Reset scores
        GameInfo.PlayerOneScore = 0;
        GameInfo.PlayerTwoScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerOneScoreText.SetText("" + GameInfo.PlayerOneScore);
        PlayerTwoScoreText.SetText("" + GameInfo.PlayerTwoScore);
    }
}
