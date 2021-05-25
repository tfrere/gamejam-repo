using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro; 

public class InstanciatePlayersMenuController : MonoBehaviour
{

    // public PlayerInputManager playerInputManager; 
    public string targetScene;
    private bool isReadyToPlay = false;
    public int numberOfInstanciatedPlayers = 0;
    public bool isConfigurationFinished = false;
    public List<GameObject> playerSpawnPoints;

    public List<TextMeshPro> playerSpawnTexts;

    public TextMeshPro startText;


    public void HandlePlayerJoin(PlayerInput playerInput) {
      Player player = GameObject.Find("PlayerConfiguration-" + numberOfInstanciatedPlayers).GetComponent<PlayerInstanciationController>().handleInstanciate(numberOfInstanciatedPlayers, playerSpawnPoints[numberOfInstanciatedPlayers].transform.position);
      player.isInvicible = true;
      playerSpawnTexts[numberOfInstanciatedPlayers].text = "";
      numberOfInstanciatedPlayers ++;
    }

  void Start() {
      startText.text = "";
  }

    void UpdateReadyState() {
      if(!isReadyToPlay) {
        isReadyToPlay = true;
        startText.text = "Press [space] to Start";
      }
    }

    void Update()
    {
      if(!isConfigurationFinished) {
        if(numberOfInstanciatedPlayers >= 2) {
          Invoke("UpdateReadyState", .5f);
        }
        if(isReadyToPlay) {
          if(Input.GetKey("space")) {
            isConfigurationFinished = true;
            GameInfo.sceneToLoad = targetScene;
            GameInfo.numberOfPlayers = numberOfInstanciatedPlayers;
            SceneManager.LoadScene("LoadingSceneWithTransition");
          }
        }
      }
    }
}
