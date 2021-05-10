using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
public class PlayerMovement : MonoBehaviour
{

    // Public configuration
    public float m_speed = 5f;

    private List<string> collisionTags =  new List<string> {"Ground"};


    // GameObject Internals
    public Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private SoundHandler soundHandler;
    private PlayerJump playerJump;
    private Player player;

    // PRIVATE COOKING
    public string orientation = "right";
    public string oldHorizontalOrientation = "right";

    public string state = "idle";
    private string prevState = "idle";
    private bool isPlayingSound = false;
    public bool isGrounded = false;

    private float inputTreshold = 0.1f;

    private Vector2 movementInputVector;

    void Start()
    {
        player = GetComponent<Player>();
        playerJump = GetComponent<PlayerJump>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        oldHorizontalOrientation = spriteRenderer.flipX ? "left" : "right";
        animator = GetComponent<Animator>();
        soundHandler = GetComponent<SoundHandler>();
    }

    public void MoveInputAction(InputAction.CallbackContext context)
    {
        print("Move!");
        movementInputVector = context.ReadValue<Vector2>();
    }

    void Update()
    {
        orientation = oldHorizontalOrientation;

        state = "idle";

        float _prevX = rigidBody.velocity.x;
        float _prevY = rigidBody.velocity.y;

        float[] list = {Vector3.Angle(movementInputVector , Vector3.left),Vector3.Angle(movementInputVector , Vector3.right),Vector3.Angle(movementInputVector , Vector3.up),Vector3.Angle(movementInputVector , Vector3.down)};
        string[] orientstrings = {"left", "right", "up", "down"};

        float minValue = list.Min();
        int index = list.ToList().IndexOf(minValue);

        string computedOrientation = movementInputVector == Vector2.zero ? "none" : orientstrings[index];

        print(computedOrientation);

        if (computedOrientation == "left")
        {
            orientation = oldHorizontalOrientation = "left";
            if(!playerJump.isJumping) { rigidBody.velocity = new Vector2(-m_speed, _prevY); }
            if(isGrounded) { state = "walking"; }
        } 
        else if (computedOrientation == "right") { 
            orientation = oldHorizontalOrientation = "right";
            if(!playerJump.isJumping) { rigidBody.velocity = new Vector2(m_speed, _prevY); }
            if(isGrounded) { state = "walking"; }
        }
        else if (computedOrientation == "up") {
            orientation = "up";
        }
        else if (computedOrientation == "down") {
            orientation = "down";
            if(!isGrounded) { rigidBody.velocity = new Vector2(_prevX, _prevY - 0.3f); }
        }

        if (isGrounded && state == "idle") {
            rigidBody.velocity = new Vector2(0f, _prevY);
        }
        if(!isPlayingSound && state != "idle") {
            MakeSound();
        }

        spriteRenderer.flipX = orientation == "left";

        prevState = state;
        animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
        animator.SetBool("isFacingUp", orientation == "up");
        animator.SetBool("isFacingDown", orientation == "down");

    }



    void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = collisionTags.Contains(collision.gameObject.tag);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = isGrounded && collisionTags.Contains(collision.gameObject.tag) ? false : isGrounded;
    }
    void MakeSound() {
        if(state == "walking") {
            isPlayingSound = true;
            soundHandler.ChangeTheSound(Random.Range(0, 5));
        }
        Invoke("MakeSoundActivation", .3f);
    }
    void MakeSoundActivation()
    {
        isPlayingSound = false;
    }


}
