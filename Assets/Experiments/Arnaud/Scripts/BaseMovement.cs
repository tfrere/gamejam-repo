using UnityEngine;

public class BaseMovement : MonoBehaviour
{

    // Public configuration
    // INPUTS
    public string leftInput = "q";
    public string rightInput = "d";
    public string topInput = "z";
    public string bottomInput = "s";

    public float m_speed = 5f;
    public float m_jump = 10.0f;
    public float m_fall = 20.0f;

    public string[] collisionTags = { "Wall", "Ground" };

    // GameObject Internals
    private Rigidbody2D rbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;




    // PRIVATE COOKING
    private bool facingLeft = false;
    private bool isGrounded = false;
    private bool isOnWall = false;
    private float _defaultMass;



    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        _defaultMass = rbody.mass;
    }

    // Update is called once per frame
    void Update()
    {

        float _prevX = rbody.velocity.x;
        float _prevY = rbody.velocity.y;

        if (Input.GetKey(leftInput))
        {
            rbody.velocity = new Vector2(-m_speed, _prevY);
            facingLeft = true;
        }
        if (Input.GetKey(rightInput))
        { 
            rbody.velocity = new Vector2(m_speed, _prevY);
            facingLeft = false;
        }
        if (Input.GetKey(topInput) && (isGrounded || isOnWall))
        {
            rbody.velocity = new Vector2(_prevX, m_jump);

        }
        if (Input.GetKey(bottomInput))
        {
            rbody.velocity = new Vector2(_prevX, -m_fall);
        }


        // Sprite direction
        spriteRenderer.flipX = facingLeft;

        // Rigidbody update
        if (isOnWall || isGrounded)
        {
            rbody.mass = 0.5f * _defaultMass;
        } else
        {
            rbody.mass = _defaultMass;
        }

        // ANIMATOR
        animator.SetFloat("Speed", Mathf.Abs(rbody.velocity.x));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // print("OnCollisionEnter2D - " + collision.gameObject.tag);
        // Enter -> So if tag is present, that's good
        isGrounded = collision.gameObject.tag == "Ground";
        isOnWall = collision.gameObject.tag == "Wall";
    }
    
    void OnCollisionStay2D(Collision2D collision)
    {
        // print("OnCollisionStay2D - " + collision.gameObject.tag);
        // Enter -> So if tag is present, that's good
        isGrounded = collision.gameObject.tag == "Ground";
        isOnWall = collision.gameObject.tag == "Wall";
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // print("OnCollisionExit2D - " + collision.gameObject.tag);
        // Exit -> So if value was previsouly set and current collision tag if present, reset the value, else keep it like it is
        isGrounded = isGrounded && collision.gameObject.tag == "Ground" ? false : isGrounded;
        isOnWall = isOnWall && collision.gameObject.tag == "Wall" ? false : isOnWall;
    }


}
