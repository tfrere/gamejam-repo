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
    public List<GameObject> playerSpawnPoints;
    public List<TextMeshPro> playerSpawnTexts;
    public Cooldown cooldown;

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
    void OnEnable()
    {
        GameEvents.current.OnUIStart += LaunchCooldown;
    }
    void OnDisable()
    {
        GameEvents.current.OnUIStart -= LaunchCooldown;
    }

    void LaunchCooldown()
    {
        if (numberOfInstanciatedPlayers >= 2)
        {
            Invoke("UpdateReadyState", 5f);
            cooldown.StartCooldown(5f);
        }
    }

    void UpdateReadyState()
    {
        isReadyToPlay = true;
    }

    void Update()
    {
        if (isReadyToPlay)
        {
            GameEvents.current.UISoundTrigger("validate");
            GameInfo.sceneToLoad = targetScene;
            GameInfo.numberOfPlayers = numberOfInstanciatedPlayers;
            SceneManager.LoadScene("LoadingSceneWithTransition");
        }
    }
}
