using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInstanciationController : MonoBehaviour
{
    public List<GameObject> playerPrefabs;
    private GameObject playerGameObject;
    private Player playerClass;

    public Player handleInstanciate(int index, Vector3 spawn) {
        playerGameObject = Instantiate(playerPrefabs[index], spawn, Quaternion.identity);
        playerGameObject.name = playerGameObject.name.Replace("(Clone)","").Trim();
        playerClass = playerGameObject.GetComponent<Player>();
        playerClass.index = index;
        return playerClass;
    }

    public void OnStart(InputAction.CallbackContext context)
    {
        print("start");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(playerClass) {
            playerClass.OnJump(context); 
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if(playerClass) {
            playerClass.OnThrow(context); 
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(playerClass) {
            playerClass.OnMove(context); 
        }
    }

    public void OnPunch(InputAction.CallbackContext context)
    {
        if(playerClass) {
            playerClass.OnPunch(context); 
        }
    }
}
