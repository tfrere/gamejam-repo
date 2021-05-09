using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public interface IPlayerMovement
{
    public void OnMove(InputValue value);
    public void OnJump(InputValue value);
    public void CancelJump();
}


public class PlayerMovementHandler: MonoBehaviour, IPlayerMovement
{
    public Vector2 moveInputs = Vector2.zero;
    public bool JumpOnMoveUp { get; set; }

    public void OnMove(InputValue value)
    {
        Debug.Log("On move" + value.Get<Vector2>().ToString());

        if (JumpOnMoveUp)
        {
            moveInputs = value.Get<Vector2>();
        } else
        {
            moveInputs.x = value.Get<Vector2>().x;
        }
        
    }
    public void OnJump(InputValue value)
    {
        Debug.Log("On Jump" + value.Get().ToString() + " " + value.isPressed);
        moveInputs.Set(moveInputs.x, value.Get<float>());
    }
    public void CancelJump()
    {
        moveInputs.Set(moveInputs.x, 0);
    }
}



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


    // MISC
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    // EXPERIMENT
    



    



    // END OF EXPERIMENT

    void Start()
    {
        // BASE
        rb2D = GetComponent<Rigidbody2D>();
        groundCollider = GetComponent<CircleCollider2D>();
        contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(mask);


        // MISC
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();


        // Player inputs
        pmh = gameObject.AddComponent<PlayerMovementHandler>() as PlayerMovementHandler;
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
            Vector2 jumpScaler = new Vector2(1.7f, 2.5f) * m_speed;
            // Initial jump : Apply the scaler to the provided input
            Vector2 jumpImpulsion = Vector2.Scale(normalizedMove, jumpScaler);
            // Aerial control : Apply new normlalized X, or get back prebious velocity
            Vector2 aerialControl = normalizedMove.x != 0 ? new Vector2(normalizedMove.x * jumpScaler.x, prevVelocity.y) : prevVelocity;
            rb2D.velocity = isFirstFrame ? jumpImpulsion : aerialControl;


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
                Vector2 fastFallScaler = new Vector2(0, -5) * m_speed;
                rb2D.velocity = fastFallScaler;
                return;
            }
            // Apply classic gravity effect 
            // Keep previous applied X / Modify Y value
            Vector2 classicFall = new Vector2(prevVelocity.x, -2f * m_speed);
            rb2D.velocity = classicFall;
            return;
        }

        // CLassic case to move
        Vector2 moveScaler = new Vector2(3, 3) * m_speed;
        rb2D.velocity = Vector2.Scale(normalizedMove, moveScaler);
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
        pmh.CancelJump();
    }

    

    /*
     * MISC
     */
    public void SetSpriteDirection(float x)
    {
        spriteRenderer.flipX = x < 0;
    }

}
