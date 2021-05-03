using UnityEngine;


// TO DO 

// On punch
// Instanciate collider 
// collider with sprite animation

public class PlayerMovement : MonoBehaviour
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
    private SoundHandler soundHandler;

    // PRIVATE COOKING
    public bool facingLeft = false;
    private bool isGrounded = false;
    private bool isOnWall = false;
    private float _defaultMass;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        soundHandler = GetComponent<SoundHandler>();

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
             soundHandler.ChangeTheSound(0);
        }
        if (Input.GetKey(rightInput))
        { 
            rbody.velocity = new Vector2(m_speed, _prevY);
            facingLeft = false;
             soundHandler.ChangeTheSound(0);
        }
        if (Input.GetKey(topInput) && (isGrounded || isOnWall))
        {
            rbody.velocity = new Vector2(_prevX, m_jump);
            soundHandler.ChangeTheSound(2);
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
        isGrounded = collision.gameObject.tag == "Ground";
        isOnWall = collision.gameObject.tag == "Wall";
        if(isOnWall) {
            soundHandler.ChangeTheSound(1);
        }
    }
    
    void OnCollisionStay2D(Collision2D collision)
    {
        // print("OnCollisionStay2D - " + collision.gameObject.tag);
        isGrounded = collision.gameObject.tag == "Ground";
        isOnWall = collision.gameObject.tag == "Wall";
        if(collision.gameObject.tag == "Death") {
            if(this.gameObject.name == "Player(Clone)"){
                GameInfo.PlayerOneScore--;
                // soundHandler.ChangeTheSound(Random.Range(3, 6));
                Destroy(this.gameObject.gameObject);
            }
            if(this.gameObject.name == "Player2(Clone)"){
                GameInfo.PlayerTwoScore--;
                // soundHandler.ChangeTheSound(Random.Range(3, 6));
                Destroy(this.gameObject.gameObject);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // print("OnCollisionExit2D - " + collision.gameObject.tag);
        isGrounded = isGrounded && collision.gameObject.tag == "Ground" ? false : isGrounded;
        isOnWall = isOnWall && collision.gameObject.tag == "Wall" ? false : isOnWall;
    }


}
