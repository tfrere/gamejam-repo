using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TestController : MonoBehaviour
{
    public GameObject PlayerOne;
    private string playerOneName;
    public GameObject PlayerTwo;
    private string playerTwoName;

    public GameObject PlayerOneSpawn;
    public GameObject PlayerTwoSpawn;

    public TextMeshPro PlayerOneScoreText;
    public TextMeshPro PlayerTwoScoreText;

    void Start()
    {
        GameInfo.PlayerOneScore = 0;
        GameInfo.PlayerTwoScore = 0;
        playerOneName = PlayerOne.name;
        playerTwoName = PlayerTwo.name;
 }

    void Update()
    {
        PlayerOneScoreText.SetText("" + GameInfo.PlayerOneScore);
        PlayerTwoScoreText.SetText("" + GameInfo.PlayerTwoScore);

        if (GameObject.Find("PlayerOne") == null) {
            // Destroy(GameObject.Find("PlayerOne"));    
            GameObject cloneOne = Instantiate(PlayerOne, PlayerOneSpawn.transform.position, Quaternion.identity);
            cloneOne.name = playerOneName;
        }
        if (GameObject.Find("PlayerTwo") == null) {
            // Destroy(GameObject.Find("PlayerTwo"));    
            GameObject cloneTwo = Instantiate(PlayerTwo, PlayerTwoSpawn.transform.position, Quaternion.identity);
            cloneTwo.name = playerTwoName;
        }
    }

}
