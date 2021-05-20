using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInstanciationController : MonoBehaviour
{

    public GameObject playerOnePrefab;
    public GameObject playerTwoPrefab;

    private GameObject instance;

    private PlayerJump playerJump;
    private PlayerThrow playerThrow;
    private PlayerMovement playerMove;
    private PlayerOrientation playerOrientation;
    private PlayerPunch playerPunch;

    private int playerIndex;

    void Start()
    {
        if(this.gameObject.name.Contains("0")) {
            playerIndex = 0;
        }
        else if(this.gameObject.name.Contains("1")) {
            playerIndex = 1;
        }
    }

    public GameObject handleInstanciate(int index, Vector3 spawn) {
        if(index == 0) {
            instance = Instantiate(playerOnePrefab, spawn, Quaternion.identity);
            playerJump = instance.GetComponent<PlayerJump>();
            playerThrow = instance.GetComponentsInChildren<PlayerThrow>()[0];
            playerMove = instance.GetComponent<PlayerMovement>();
            playerOrientation = instance.GetComponent<PlayerOrientation>();
            playerPunch = instance.GetComponentsInChildren<PlayerPunch>()[0];
        }
        else if(index == 1) {
            instance = Instantiate(playerTwoPrefab, spawn, Quaternion.identity);
            playerJump = instance.GetComponent<PlayerJump>();
            playerThrow = instance.GetComponentsInChildren<PlayerThrow>()[0];
            playerMove = instance.GetComponent<PlayerMovement>();
            playerOrientation = instance.GetComponent<PlayerOrientation>();
            playerPunch = instance.GetComponentsInChildren<PlayerPunch>()[0];
        }
        return instance;
    }

    public GameObject handleInstanciate(int index) {
        if(index == 0) {
            instance = Instantiate(playerOnePrefab, new Vector3(0,0,0), Quaternion.identity);
            playerJump = instance.GetComponent<PlayerJump>();
            playerThrow = instance.GetComponentsInChildren<PlayerThrow>()[0];
            playerMove = instance.GetComponent<PlayerMovement>();
            playerOrientation = instance.GetComponent<PlayerOrientation>();
            playerPunch = instance.GetComponentsInChildren<PlayerPunch>()[0];
        }
        else if(index == 1) {
            instance = Instantiate(playerTwoPrefab, new Vector3(0,0,0), Quaternion.identity);
            playerJump = instance.GetComponent<PlayerJump>();
            playerThrow = instance.GetComponentsInChildren<PlayerThrow>()[0];
            playerMove = instance.GetComponent<PlayerMovement>();
            playerOrientation = instance.GetComponent<PlayerOrientation>();
            playerPunch = instance.GetComponentsInChildren<PlayerPunch>()[0];
        }
        return instance;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("player have to jump");
        playerJump.JumpInputAction(context);
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        Debug.Log("player have to throw");
        playerThrow.ThrowInputAction(context);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("player have to move");
        playerMove.MoveInputAction(context);
        playerOrientation.MoveInputAction(context);
    }

    public void OnPunch(InputAction.CallbackContext context)
    {
        Debug.Log("player have to punch");
        playerPunch.PunchInputAction(context);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
