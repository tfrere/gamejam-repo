using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public bool isMakingAnAction = false;

    public bool isInvicible = false;

    public string leftInput = "q";
    public string rightInput = "d";
    public string topInput = "z";
    public string bottomInput = "s";

    public string punchInput = "e";
    public string jumpInput = "x";
    public string throwInput = "q";

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