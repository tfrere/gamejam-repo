using System.Collections.Generic;
using UnityEngine;


public class __ReworkedMovement: MonoBehaviour
{

    // Public configuration
    public float m_speed = 10f;
    public LayerMask mask;


    // BASE
    private Rigidbody2D rb2D;
    private PlayerMovementHandler pmh;
    private ContactFilter2D contactFilter;
    private CircleCollider2D groundCollider;


    // BASE State
    private bool isGrounded = false;
    private bool hasJumped = false;
    private bool hasFastFall = false;
    private float t = 0.0f;
    private float JUMP_TIMER = 0.6f;

    // BASE Vectors -> Apply scale on m_speed on start method
    private Vector2 SCALER_MOVE = new Vector2(3, 3);
    private Vector2 SCALER_JUMP = new Vector2(1.7f, 2.5f);
    private Vector2 SCALER_FASTFALL = new Vector2(0, -5);
    private Vector2 SCALER_CLASSICFALL = new Vector2(0, -2f);


    // MISC
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        // BASE
        rb2D = GetComponent<Rigidbody2D>();
        groundCollider = GetComponent<CircleCollider2D>();
        contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(mask);

        // VECTOR INITIALIZATION
        SCALER_MOVE *= m_speed;
        SCALER_JUMP *= m_speed;
        SCALER_FASTFALL *= m_speed;
        SCALER_CLASSICFALL *= m_speed;


        // MISC
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();


        // Player inputs
        pmh = gameObject.AddComponent<PlayerMovementHandler>();
        //pmh.JumpOnMoveUp = true;
        //pmh.ForceFallOnMoveDown = true;
    }

    void FixedUpdate()
    {
        // Prev velocity - not normalized
        Vector2 prevVelocity = rb2D.velocity;

        // Normalize inputs
        Vector2 normalizedMove = pmh.moveInputs;


        // Manage BASE state and MISC State
        SetIsGrounded();
        SetSpriteDirection(normalizedMove.x);
        bool isJumping = isGrounded && normalizedMove.y > 0;
        bool isFastFalling = !isGrounded && normalizedMove.y < 0;

        if (isJumping)
        {
            hasJumped = true;
        }
        if (isFastFalling)
        {
            ResetHasJumped();
            hasFastFall = true;
        }



        if (hasJumped)
        {
            // Jump
            bool isFirstFrame = t == 0;

            if (isFirstFrame)
            {
                // Initial jump : Apply the scaler to the provided input
                rb2D.velocity = Vector2.Scale(normalizedMove, SCALER_JUMP);
            } else
            {
                // Aerial control : Apply new normlalized X, or get back prebious velocity
                rb2D.velocity = new Vector2(normalizedMove.x * SCALER_JUMP.x, prevVelocity.y);
            }

            // Reset delay
            t += Time.deltaTime;
            if (t > JUMP_TIMER)
            {
                ResetHasJumped();
            }
            return;
        }


        // Aerial effect
        if (!isGrounded)
        {
            if (hasFastFall)
            {
                // Apply the fast fall effect
                rb2D.velocity = SCALER_FASTFALL;
                return;
            }
            // Apply classic gravity effect 
            // Keep previous applied X / Modify Y value
            SCALER_CLASSICFALL.x = prevVelocity.x;
            rb2D.velocity = SCALER_CLASSICFALL;
            return;
        }

        // CLassic case to move
        rb2D.velocity = Vector2.Scale(normalizedMove, SCALER_MOVE);
    }



    /*
     * Internal state management (linked to Rigidbody essentially)
     */
    public void SetIsGrounded()
    {
        // Check if the collider is overlapping a specific layer based on the contactFilter
        List<Collider2D> results = new List<Collider2D>();
        int listSize = groundCollider.OverlapCollider(contactFilter, results);
        // If the provided list size is positive, than the groundCollider is touching the ground mask
        isGrounded = listSize > 0;
        if (isGrounded)
        {
            ResetHasJumped();
            hasFastFall = true;
        }
    }

    public void ResetHasJumped()
    {
        t = 0f;
        hasJumped = false;
        pmh.CancelJumpInput();
    }

    

    /*
     * MISC
     */
    public void SetSpriteDirection(float x)
    {
        spriteRenderer.flipX = x < 0;
    }

}
