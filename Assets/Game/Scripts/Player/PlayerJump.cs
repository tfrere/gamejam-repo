using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{

    public float jumpForce = 1.2f;
    public float wallJumpRepulseForce = 3.0f;

    // GameObject Internals
    private Player player;
    private Rigidbody2D rbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private PlayerMovement playerMovement;
    private SoundHandler soundHandler;

    private string oldHorizontalOrientation;
    public bool isOnWall = false;
    public bool isJumping = false;

    public bool hasToJump = false;

    // Acceptable difference in degrees to differentiate if player comes from top
     private float contactThreshold = 30;


    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        soundHandler = GetComponent<SoundHandler>();
    }

    void Update()
    {
        if(hasToJump && !isJumping && !isOnWall) {
            MakeJump();
        }
    }

    bool IsCollidingFromTop(Vector3 vector) {
        if (Vector3.Angle(vector, Vector3.up) <= contactThreshold)
            return true;
        return false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        isOnWall = !IsCollidingFromTop(collision.contacts[0].normal);
        
        if(hasToJump && !isJumping && (playerMovement.isGrounded || isOnWall))
            MakeWallJump(collision);            
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isOnWall = false;
    }

    public void JumpInputAction(InputAction.CallbackContext context)
    {
        if(context.performed && !isJumping && (playerMovement.isGrounded || isOnWall)) {
            print("Jump!");
            hasToJump = true;
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
        isJumping = true;
        // animator.SetBool("isJumping", true);
        Vector2 repulseVector = new Vector2();
        string orientation = this.gameObject.transform.position.x - collision.contacts[0].point.x > 0 ? "left" : "right";
        if(orientation == "left") { 
            repulseVector = new Vector2(wallJumpRepulseForce, jumpForce); 
            playerMovement.oldHorizontalOrientation = "right";
        }
        if(orientation == "right") { 
            repulseVector = new Vector2(-wallJumpRepulseForce, jumpForce); 
            playerMovement.oldHorizontalOrientation = "left";
        }
        rbody.velocity = repulseVector;
        soundHandler.ChangeTheSound(7);
        yield return new WaitForSeconds(0.3f);
        // animator.SetBool("isJumping", false);
        isJumping = false;
        hasToJump = false;
    }

    IEnumerator MakeJumpActivation()
    {
        isJumping = true;
        // animator.SetBool("isJumping", true);
        rbody.velocity = new Vector2(rbody.velocity.x, jumpForce);
        soundHandler.ChangeTheSound(7);
        yield return new WaitForSeconds(0.3f);
        // animator.SetBool("isJumping", false);
        isJumping = false;
        hasToJump = false;
    }
}
