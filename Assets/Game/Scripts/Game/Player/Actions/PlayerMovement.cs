using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
public class PlayerMovement : MonoBehaviour
{

    private Player player;
    private Vector2 movementInputVector;

    void Start()
    {
        player = GetComponent<Player>();
    }

    public void MoveInputAction(InputAction.CallbackContext context)
    {
        movementInputVector = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {

        player.currentState = "idle";

        float _prevX = player.rb.velocity.x;
        float _prevY = player.rb.velocity.y;

        if (!player.isJumping &&
            (player.currentOrientation == "left"
            || player.currentOrientation == "right")
            )
        {
            // if(!player.isJumping) { updateVelocity(new Vector2(-player.moveSpeed, _prevY)); }
            float direction = player.currentOrientation == "left" ? -player.moveSpeed : player.moveSpeed;
            player.updateVelocity(new Vector2(direction, _prevY));
            if(player.isGrounded) { player.currentState = "walking"; }
        } 
        if (player.currentOrientation == "down") {
            if(!player.isGrounded) { player.updateVelocity(new Vector2(_prevX, _prevY - 0.3f)); }
        }

    }

}
