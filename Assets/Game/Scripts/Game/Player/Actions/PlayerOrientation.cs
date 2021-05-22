using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
public partial class PlayerOrientation : MonoBehaviour
{

    private Player player;

    private Vector2 movementInputVector;

    private string[] ORIENTATION_NAMES = {"left", "right", "up", "down"};

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
        float[] list = {
            Vector3.Angle(movementInputVector, Vector3.left),
            Vector3.Angle(movementInputVector, Vector3.right),
            Vector3.Angle(movementInputVector, Vector3.up),
            Vector3.Angle(movementInputVector, Vector3.down)
        };

        float minValue = list.Min();
        int index = list.ToList().IndexOf(minValue);

        player.currentOrientation = movementInputVector == Vector2.zero ? "none" : ORIENTATION_NAMES[index];

        if (player.currentOrientation == "left")
            player.oldHorizontalOrientation = "left";
        else if (player.currentOrientation == "right")
            player.oldHorizontalOrientation = "right";
    }

}
