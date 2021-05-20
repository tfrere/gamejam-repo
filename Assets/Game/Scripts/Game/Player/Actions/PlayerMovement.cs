using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
public class PlayerMovement : MonoBehaviour
{

    // Public configuration
    public float m_speed = 5f;

    private List<string> collisionTags =  new List<string> {"Ground", "Player"};


    // GameObject Internals
    public Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private SoundHandler soundHandler;
    private PlayerJump playerJump;
    private PlayerOrientation playerOrientation;
    private Player player;


    public string state = "idle";
    public string prevState = "idle";
    private bool isPlayingSound = false;
    public bool isGrounded = false;

    private float inputTreshold = 0.1f;

    private Vector2 movementInputVector;

    void Start()
    {
        player = GetComponent<Player>();
        playerJump = GetComponent<PlayerJump>();
        playerOrientation = GetComponent<PlayerOrientation>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        soundHandler = GetComponent<SoundHandler>();
        if(this.gameObject.transform.position.x > 0) {
            spriteRenderer.flipX = true;
        }
        else {
            spriteRenderer.flipX = false;
        }
    }

    public void MoveInputAction(InputAction.CallbackContext context)
    {
        print("Move!");
        movementInputVector = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {

        state = "idle";

        float _prevX = rigidBody.velocity.x;
        float _prevY = rigidBody.velocity.y;


        if (playerOrientation.currentOrientation == "left")
        {
            if(!playerJump.isJumping) { updatePlayerVelocity(new Vector2(-m_speed, _prevY)); }
            if(isGrounded) { state = "walking"; }
        } 
        else if (playerOrientation.currentOrientation == "right") { 
            if(!playerJump.isJumping) { updatePlayerVelocity(new Vector2(m_speed, _prevY)); }
            if(isGrounded) { state = "walking"; }
        }
        // else {
        //     if (isGrounded && state == "idle") {
        //         //  && prevState == "walking"
        //         updatePlayerVelocity(new Vector2(0f, _prevY));
        //     }
        // }
        if (playerOrientation.currentOrientation == "down") {
            if(!isGrounded) { updatePlayerVelocity(new Vector2(_prevX, _prevY - 0.3f)); }
        }
        if(!isPlayingSound && state == "walking") {
            MakeSound();
        }

        spriteRenderer.flipX = playerOrientation.oldHorizontalOrientation == "left";

        prevState = state;
        animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
        animator.SetBool("isFacingUp", playerOrientation.currentOrientation == "up");
        animator.SetBool("isFacingDown", playerOrientation.currentOrientation == "down");
    }

    void updatePlayerVelocity(Vector2 newVector) {
        rigidBody.velocity = newVector;
        // rigidbody.AddForce(Velocity, ForceMode.VelocityChange);
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = collisionTags.Contains(collision.gameObject.tag);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    void MakeSound() {
        isPlayingSound = true;
        soundHandler.ChangeTheSound(Random.Range(0, 5));
        Invoke("MakeSoundActivation", .3f);
    }

    void MakeSoundActivation()
    {
        isPlayingSound = false;
    }

}
