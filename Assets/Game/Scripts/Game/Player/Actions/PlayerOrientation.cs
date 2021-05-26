using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
public partial class PlayerOrientation : MonoBehaviour
{

    private Player player;

    private Vector2 movementInputVector;

    void Start()
    {
        player = this.gameObject.GetComponent<Player>();

        // if player is instanciated at the right side of the screen
        if(this.gameObject.transform.position.x > 0)
            player.oldHorizontalOrientation = "left";
        else
            player.oldHorizontalOrientation = "right";

    }

    public void MoveInputAction(InputAction.CallbackContext context)
    {
        movementInputVector = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {

        player.currentOrientation = NormalizeOrientation.Normalize(movementInputVector);

        if (player.currentOrientation == "left")
            player.oldHorizontalOrientation = "left";
        else if (player.currentOrientation == "right")
            player.oldHorizontalOrientation = "right";
    }

}
