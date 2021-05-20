using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
public class PlayerOrientation : MonoBehaviour
{

    public string currentOrientation = "none";
    public string oldHorizontalOrientation = "right";

    private Vector2 movementInputVector;

    private string[] ORIENTATION_NAMES = {"left", "right", "up", "down"};

    void Start()
    {
        // if player is instanciated at the right side of the screen
        if(this.gameObject.transform.position.x > 0)
            oldHorizontalOrientation = "left";
        else
            oldHorizontalOrientation = "right";
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

        currentOrientation = movementInputVector == Vector2.zero ? "none" : ORIENTATION_NAMES[index];

        if (currentOrientation == "left")
            oldHorizontalOrientation = "left";
        else if (currentOrientation == "right")
            oldHorizontalOrientation = "right";
    }

}
