using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{

    private PlayerInput playerInput;
    public bool isMakingAnAction = false;

    public bool isInvicible = false;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        this.gameObject.name = this.gameObject.name.Replace("(Clone)","").Trim();
        if(this.gameObject.name == "PlayerOne") {
            print("player one instanciation");
            GameInfo.PlayerOneArrows = GameInfo.InitialArrows;

        }
        else if(this.gameObject.name == "PlayerTwo") {
            print("player two instanciation");
            GameInfo.PlayerTwoArrows = GameInfo.InitialArrows;
            // playerInput.Enable();
        }
    }
    
}
