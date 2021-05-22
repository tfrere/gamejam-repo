using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rigidBody;

    private bool hasToJump = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        // perform normal jump from ground
        if(hasToJump && player.isGrounded && !player.isJumping && !player.isOnWall) {
            MakeJump();
        }
    }

    public void JumpInputAction(InputAction.CallbackContext context)
    {
        if(context.performed && !player.isJumping && (player.isGrounded || player.isOnWall)) {
            hasToJump = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // perform wall jump from wall
        if(hasToJump && !player.isJumping && player.isOnWall) { // && !player.isGrounded
            MakeWallJump(collision);            
        }
    }

    void MakeJump() {
        StartCoroutine(MakeJumpActivation());
    }
    
    void MakeWallJump(Collision2D collision) {
        StartCoroutine(MakeWallJumpActivation(collision));
    }
    
    IEnumerator MakeWallJumpActivation(Collision2D collision)
    {
        player.isJumping = true;
        Vector2 repulseVector = new Vector2();
        string collideOrientation = this.gameObject.transform.position.x - collision.contacts[0].point.x > 0 ? "left" : "right";
        repulseVector = new Vector2(collideOrientation == "left" ? player.wallJumpRepulseForce : -player.wallJumpRepulseForce, player.jumpForce); 
        player.currentOrientation = collideOrientation == "left" ? "right" : "left";
        player.oldHorizontalOrientation = player.currentOrientation;
        player.updateVelocity(repulseVector);
        yield return new WaitForSeconds(player.jumpTime);
        player.isJumping = false;
        hasToJump = false;
    }

    IEnumerator MakeJumpActivation()
    {
        player.isJumping = true;
        player.updateVelocity(new Vector2(rigidBody.velocity.x, player.jumpForce));
        yield return new WaitForSeconds(player.jumpTime);
        player.isJumping = false;
        hasToJump = false;
    }
}


