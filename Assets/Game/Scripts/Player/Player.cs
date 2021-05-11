using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{

    public bool isMakingAnAction = false;
    public bool isInvicible = false;

    void Start()
    {
        print("player instanciation");
        this.gameObject.name = this.gameObject.name.Replace("(Clone)","").Trim();
        if(this.gameObject.name == "PlayerOne") {
            GameInfo.PlayerOneArrows = GameInfo.InitialArrows;

        }
        else if(this.gameObject.name == "PlayerTwo") {
            GameInfo.PlayerTwoArrows = GameInfo.InitialArrows;
        }
    }
    
}
