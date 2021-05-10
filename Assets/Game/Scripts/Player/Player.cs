using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public bool isMakingAnAction = false;

    public bool isInvicible = false;

    void Start()
    {
        if(this.gameObject.name == "PlayerOne") {
            GameInfo.PlayerOneArrows = GameInfo.InitialArrows;
        }
        else if(this.gameObject.name == "PlayerTwo") {
            GameInfo.PlayerTwoArrows = GameInfo.InitialArrows;
        }
    }
 

}
