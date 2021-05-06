using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Public configuration
    // INPUTS
    public string leftInput = "q";
    public string rightInput = "d";
    public string topInput = "z";
    public string bottomInput = "s";
    public string jumpInput = "x";

    public float m_speed = 5f;

    private List<string> collisionTags =  new List<string> {"Ground"};


    // GameObject Internals
    public Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private SoundHandler soundHandler;

    // PRIVATE COOKING
    public string orientation = "right";
    public string oldHorizontalOrientation = "right";

    public string state = "idle";
    private string prevState = "idle";
    private bool isPlayingSound = false;
    public bool isGrounded = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        oldHorizontalOrientation = spriteRenderer.flipX ? "left" : "right";
        animator = GetComponent<Animator>();
        soundHandler = GetComponent<SoundHandler>();

    }

    // Update is called once per frame
    void Update()
    {

        orientation = oldHorizontalOrientation;

        state = "idle";

        float _prevX = rigidBody.velocity.x;
        float _prevY = rigidBody.velocity.y;

        // IF LEFT RIGHT
        if (Input.GetKey(leftInput))
        {
            rigidBody.velocity = new Vector2(-m_speed, _prevY);
            orientation = oldHorizontalOrientation = "left";
            if(isGrounded) { state = "walking"; }
        } else if (Input.GetKey(rightInput))
        { 
            rigidBody.velocity = new Vector2(m_speed, _prevY);
            orientation = oldHorizontalOrientation = "right";
            if(isGrounded) { state = "walking"; }
        }
        if (isGrounded && state == "idle" && prevState == "walking") {
            rigidBody.velocity = new Vector2(0f, _prevY);
        }
        if (Input.GetKey(topInput))
        {
            orientation = "top";
        }
        if (Input.GetKey(bottomInput))
        {
            orientation = "bottom";
        }

        // IF ON WALL OR GROUNDED
        if(!isPlayingSound && state != "idle") {
            MakeSound();
        }

        spriteRenderer.flipX = orientation == "left";

        prevState = state;

        animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
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
        StartCoroutine(MakeSoundActivation());
    }

    IEnumerator MakeSoundActivation()
    {
        if(state == "walking") {
            isPlayingSound = true;
            soundHandler.ChangeTheSound(Random.Range(0, 5));
        }
        yield return new WaitForSeconds(0.3f);
        isPlayingSound = false;
    }


}
