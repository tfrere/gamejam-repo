using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro; 

public class InstanciatePlayersController : MonoBehaviour
{

    // public PlayerInputManager playerInputManager; 
    public string targetScene;
    public GameObject playerOnePrefab;
    public GameObject playerTwoPrefab;
    public TextMeshPro title;
    public TextMeshPro desc;

  private bool isReadyToPlay = false;

  public int numberOfInstanciatedPlayers = 0;

  public bool isConfigurationFinished = false;

    public void HandlePlayerJoin(PlayerInput playerInput) {
      if(numberOfInstanciatedPlayers == 0 ) {
        GameObject.Find("PlayerConfiguration-0").GetComponent<PlayerInstanciationController>().handleInstanciate(0, new Vector3(0,0,0));
      }
      if(numberOfInstanciatedPlayers == 1 ) {
        GameObject.Find("PlayerConfiguration-1").GetComponent<PlayerInstanciationController>().handleInstanciate(1, new Vector3(0,0,0));
      }
      numberOfInstanciatedPlayers ++;
     }

    void Start()
    {

    }

    void UpdateReadyState() {
      if(!isReadyToPlay) {
        isReadyToPlay = true;
        desc.text = "Press [space] to begin";
      }
    }

    // Update is called once per frame
    void Update()
    {
      if(!isConfigurationFinished) {
        title.text = "Players " + numberOfInstanciatedPlayers + "/2";
        if(numberOfInstanciatedPlayers == 2) {
          Invoke("UpdateReadyState", .5f);
        }
        if(isReadyToPlay) {
          if(Input.GetKey("space")) {
            isConfigurationFinished = true;
            GameInfo.sceneToLoad = targetScene;
            SceneManager.LoadScene("LoadingSceneWithTransition");
          }
        }
      }
    }
}
