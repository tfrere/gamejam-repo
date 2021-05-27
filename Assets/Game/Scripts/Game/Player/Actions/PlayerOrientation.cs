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
        if (this.gameObject.transform.position.x > 0)
            player.oldHorizontalOrientation = "left";
        else
            player.oldHorizontalOrientation = "right";

    }

    public void MoveInputAction(InputAction.CallbackContext context)
    {
        movementInputVector = context.ReadValue<Vector2>();
        // joystick sensitivity prevent
        movementInputVector = new Vector2(
            Mathf.Abs(movementInputVector.x) < 0.18 ? 0f : movementInputVector.x,
            Mathf.Abs(movementInputVector.y) < 0.18 ? 0f : movementInputVector.y);

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
