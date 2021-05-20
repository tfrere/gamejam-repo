using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{

    public float jumpForce = 1.2f;
    public float wallJumpRepulseForce = 3.0f;
    public float jumpTime = 0.3f;

    // GameObject Internals
    private Player player;
    private PlayerOrientation playerOrientation;
    private Rigidbody2D rbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private PlayerMovement playerMovement;
    private SoundHandler soundHandler;

    public bool isOnWall = false;
    public bool isJumping = false;

    public bool hasToJump = false;

    // Acceptable difference in degrees to differentiate if player comes from top
     public float contactThreshold = 30;


    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        playerMovement = GetComponent<PlayerMovement>();
        playerOrientation = GetComponent<PlayerOrientation>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        soundHandler = GetComponent<SoundHandler>();
    }

    void FixedUpdate()
    {
        if(hasToJump && playerMovement.isGrounded && !isJumping && !isOnWall) {
            MakeJump();
        }
        
    }

    bool IsOnWall(Vector3 vector) {
        if (Vector3.Angle(vector, Vector3.left) <= 10)
            return true;
        else if (Vector3.Angle(vector, Vector3.right) <= 10)
            return true;
        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isOnWall = IsOnWall(collision.contacts[0].normal);
        print("isOnWall -> " + isOnWall);
        
        if(hasToJump && !isJumping && isOnWall) {
            print("walljump");
            MakeWallJump(collision);            
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        isOnWall = IsOnWall(collision.contacts[0].normal);
        print("isOnWall -> " + isOnWall);
        
        if(hasToJump && !isJumping && isOnWall) {
            print("walljump");
            MakeWallJump(collision);            
        }
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
        string collideOrientation = this.gameObject.transform.position.x - collision.contacts[0].point.x > 0 ? "left" : "right";
        if(collideOrientation == "left") { 
            repulseVector = new Vector2(wallJumpRepulseForce, jumpForce); 
            playerOrientation.currentOrientation = playerOrientation.oldHorizontalOrientation = "right";
        }
        if(collideOrientation == "right") { 
            repulseVector = new Vector2(-wallJumpRepulseForce, jumpForce); 
            playerOrientation.currentOrientation = playerOrientation.oldHorizontalOrientation = "left";
        }
        print(collideOrientation);
        print("jump repulse vector" + repulseVector);
        // rbody.AddForce(repulseVector, ForceMode2D.Impulse);
        rbody.velocity = repulseVector;
        soundHandler.ChangeTheSound(7);
        yield return new WaitForSeconds(jumpTime);
        // animator.SetBool("isJumping", false);
        isJumping = false;
        hasToJump = false;
    }

    IEnumerator MakeJumpActivation()
    {
        isJumping = true;
        // animator.SetBool("isJumping", true);
        rbody.velocity = new Vector2(rbody.velocity.x, jumpForce);
        // rbody.AddForce(new Vector2(rbody.velocity.x, jumpForce), ForceMode2D.Impulse);
        soundHandler.ChangeTheSound(7);
        yield return new WaitForSeconds(jumpTime);
        // animator.SetBool("isJumping", false);
        isJumping = false;
        hasToJump = false;
    }
}


