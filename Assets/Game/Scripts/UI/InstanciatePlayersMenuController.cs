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


    public void HandlePlayerJoin(PlayerInput playerInput)
    {
        Player player = GameObject.Find("PlayerConfiguration-" + numberOfInstanciatedPlayers).GetComponent<PlayerInstanciationController>().handleInstanciate(numberOfInstanciatedPlayers, playerSpawnPoints[numberOfInstanciatedPlayers].transform.position);
        player.isInvicible = true;
        playerSpawnTexts[numberOfInstanciatedPlayers].text = "";
        numberOfInstanciatedPlayers++;
        StartCoroutine(HandleChangeInputScheme());
    }

    IEnumerator HandleChangeInputScheme()
    {
        yield return new WaitForSeconds(.1f);
        GameEvents.current.ChangeInputSchemeTrigger("Player");
    }

    void Start()
    {
        startText.text = "";
    }

    void OnEnable()
    {
        GameEvents.current.OnUIStart += LoadNextScene;
    }
    void OnDisable()
    {
        GameEvents.current.OnUIStart -= LoadNextScene;
    }

    void UpdateReadyState()
    {
        if (!isReadyToPlay)
        {
            isReadyToPlay = true;
            startText.text = "Press [space or start] to begin";
        }
    }

    void LoadNextScene()
    {
        if (isReadyToPlay)
        {
            isConfigurationFinished = true;
            GameInfo.sceneToLoad = targetScene;
            GameInfo.numberOfPlayers = numberOfInstanciatedPlayers;
            SceneManager.LoadScene("LoadingSceneWithTransition");
        }
    }

    void Update()
    {
        if (!isConfigurationFinished && numberOfInstanciatedPlayers >= 2)
        {
            Invoke("UpdateReadyState", .5f);
        }
    }
}
