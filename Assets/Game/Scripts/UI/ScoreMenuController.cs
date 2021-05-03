using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreMenuController : MonoBehaviour
{
    public TextMeshPro winText;
    public GameObject PlayerOneSprite;
    public GameObject PlayerTwoSprite;
      public string targetScene;

    // Start is called before the first frame update
    void Start()
    {
        PlayerOneSprite.SetActive(false);
        PlayerTwoSprite.SetActive(false);

        
        if(GameInfo.PlayerOneScore == GameInfo.MaxScore) {
            winText.SetText("Player 1 won");
            PlayerOneSprite.SetActive(true);
        }
        if(GameInfo.PlayerTwoScore == GameInfo.MaxScore) {
            winText.SetText("Player 2 won");
            PlayerTwoSprite.SetActive(true);
        }

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
