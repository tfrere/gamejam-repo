using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreMenuController : MonoBehaviour
{
    public TextMeshPro winText;

    public TextMeshPro playerOneScore;
    public TextMeshPro playerTwoScore;

    public GameObject playerOneBox;
    public GameObject playerTwoBox;

      public string targetScene;

    // Start is called before the first frame update
    void Start()
    {
        if(GameInfo.PlayerOneScore == GameInfo.MaxScore) {
            winText.SetText("Player 1 won");
            // playerOneBox.transform.translate = new Vector3(playerOneBox.transform.translate.x, playerOneBox.transform.translate.y - 1f, playerOneBox.transform.translate.z);
        }
        if(GameInfo.PlayerTwoScore == GameInfo.MaxScore) {
            winText.SetText("Player 2 won");
            // playerTwoBox.transform.translate = new Vector3(playerTwoBox.transform.translate.x, playerTwoBox.transform.translate.y - 1f, playerTwoBox.transform.translate.z);
        }
        playerOneScore.text = GameInfo.PlayerOneScore + "";
        playerTwoScore.text = GameInfo.PlayerTwoScore + "";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("space")) {
            GameInfo.sceneToLoad = targetScene;
            SceneManager.LoadScene("LoadingSceneWithTransition");
        }
    }
}
