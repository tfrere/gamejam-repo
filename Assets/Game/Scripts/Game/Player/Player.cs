using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [HideInInspector] public int index;

    [Header("Internal States")]
    public bool isMakingAnAction = false;
    public bool isInvicible = false;
    public bool isGrounded = false;
    public bool isOnWall = false;
    public bool isDead = false;

    [Header("States")]
    public string currentState = "idle"; // walking, jumping, dead
    public string currentOrientation = "none";
    public string oldHorizontalOrientation = "right";

    [Header("Move")]
    #region moveVariables

    [Tooltip("WalkSpeed, usually a 5f")]
    public float moveSpeed = 5f;

    #endregion

    [Header("Jump")]
    #region jumpVariables

    public float jumpForce = 11.2f;
    public float wallJumpRepulseForce = 2.0f;
    public float jumpTime = 0.2f;
    public bool isJumping = false;

    #endregion

    [Header("Punch")]
    #region punchVariables

    public bool isPunching = false;
    public float accelerationOnPunch = 15.0f;
    public float punchRepulseForce = 15f;
    public float punchLerpTime = 0.10f;

    #endregion

    [Header("Throw")]
    #region throwVariables

    public bool isThrowing = false;
    public float throwSpeed = 50;
    public GameObject throwObject;

    #endregion


    // CONSTANTS
    private List<string> GROUND_TAGS = new List<string> { "Ground", "Player", "Bamboo" };

    // ACTIONS
    private PlayerJump playerJump;
    private PlayerThrow playerThrow;
    private PlayerMovement playerMove;
    private PlayerOrientation playerOrientation;
    private PlayerPunch playerPunch;

    // COMPONENTS
    private SpriteRenderer spriteRenderer;

    private Animator animator;
    private SoundHandler soundHandler;

    [HideInInspector] public Rigidbody2D rb;

    void Start()
    {
        print(this.gameObject.name + " : instanciation ");
        GameInfo.playerArrows[index] = GameInfo.initialArrows;

        playerMove = this.gameObject.GetComponent<PlayerMovement>();
        playerOrientation = this.gameObject.GetComponent<PlayerOrientation>();
        playerJump = this.gameObject.GetComponent<PlayerJump>();
        playerThrow = this.gameObject.GetComponentsInChildren<PlayerThrow>()[0];
        playerPunch = this.gameObject.GetComponentsInChildren<PlayerPunch>()[0];

        // should be moved in a separate class ?
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        soundHandler = GetComponent<SoundHandler>();
        rb = GetComponent<Rigidbody2D>();

        // mirror spriterenderer baed on player orientation
        if (this.gameObject.transform.position.x > 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }

    void FixedUpdate()
    {
        spriteRenderer.flipX = oldHorizontalOrientation == "left";
        handleSlowFalling();
    }

    void Update()
    {
        handleAnimation();
        handleSound();
    }

    void handleSlowFalling()
    {
        // slow down falling when stuck on wall
        if (isOnWall && rb.velocity.y < -0.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.70f);
        }
    }

    void handleAnimation()
    {
        animator.SetBool("isWalking", !isOnWall && currentState == "walking");
        animator.SetBool("isJumping", isJumping && !isOnWall);
        animator.SetBool("isDead", isDead);
        animator.SetBool("isFacingUp", currentOrientation == "up" && !isJumping);
        animator.SetBool("isFacingDown", currentOrientation == "down");
        animator.SetBool("isOnWall", isOnWall);
    }

    void handleSound()
    {

        // if (isJumping && !isOnWall)
        // {
        //     soundHandler.ChangeTheSound(7);
        // }
        // else if (!isOnWall && currentState == "walking")
        // {
        //     soundHandler.ChangeTheSound(Random.Range(0, 5));
        // }
    }


    #region Actions

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log(this.gameObject.name + " : jump");
        playerJump.JumpInputAction(context);
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        Debug.Log(this.gameObject.name + " : throw");
        playerThrow.ThrowInputAction(context);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log(this.gameObject.name + " : move");
        playerOrientation.MoveInputAction(context);
        playerMove.MoveInputAction(context);
    }

    public void OnPunch(InputAction.CallbackContext context)
    {
        Debug.Log(this.gameObject.name + " : punch");
        playerPunch.PunchInputAction(context);
    }

    #endregion

    // PUBLIC METHODS
    public void updateVelocity(Vector2 newVector)
    {
        rb.velocity = newVector;
    }

    public void AddForce(Vector2 newVector, ForceMode2D forceMode)
    {
        rb.AddForce(newVector, forceMode);
    }

    // COLLISIONS
    #region Collisions
    bool IsOnWall(Vector3 vector)
    {
        if (Vector3.Angle(vector, Vector3.left) <= 10)
            return true;
        else if (Vector3.Angle(vector, Vector3.right) <= 10)
            return true;
        return false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        isOnWall = IsOnWall(collision.contacts[0].normal);
        isGrounded = GROUND_TAGS.Contains(collision.gameObject.tag);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isOnWall = false;
        isGrounded = false;
    }

    #endregion
}
