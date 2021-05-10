using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{

    public float jumpForce = 1.2f;
    public float wallJumpRepulseForce = 3.0f;
    private List<string> collisionTags =  new List<string> {"Borders", "Wall"};

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
        if(!playerMovement.isGrounded) {
            animator.SetBool("isJumping", isJumping);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {

        bool comingFromTop = false;
        // if collide from top 
        if (Vector3.Angle(collision.contacts[0].normal, Vector3.up) <= contactThreshold)
        {
            comingFromTop = true;
        }

        isOnWall = !comingFromTop;

        if(hasToJump && !isJumping && (playerMovement.isGrounded || isOnWall)) {
            MakeJump(collision);            
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isOnWall = isOnWall && collisionTags.Contains(collision.gameObject.tag) ? false : isOnWall;
    }

    public void JumpInputAction(InputAction.CallbackContext context)
    {
        if(context.performed && !isJumping && (playerMovement.isGrounded || isOnWall)) {
            print("Jump!");
            hasToJump = true;
        }
    }

    void MakeJump(Collision2D collision) {
        StartCoroutine(MakeJumpActivation(collision));
    }

    IEnumerator MakeJumpActivation(Collision2D collision)
    {
        isJumping = true;
        if (isOnWall) {

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

            // rbody.velocity = new Vector2(0,0);
            rbody.velocity = repulseVector;
        }
        else {
            rbody.velocity = new Vector2(rbody.velocity.x, jumpForce);
        }
        soundHandler.ChangeTheSound(7);
        yield return new WaitForSeconds(0.3f);
        isJumping = false;
        hasToJump = false;
    }
    

}
