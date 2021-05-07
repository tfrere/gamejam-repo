using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
     public Player Player;
     public PlayerMovement PlayerMovement;
     public PlayerJump PlayerJump;
     public PlayerDeath PlayerDeath;
     private Collider2D PunchCollider;
     private SpriteRenderer SpriteRenderer;
     private Animator Animator;
     private SoundHandler soundHandler;

     private bool isPunching;
    public float accelerationOnPunch = 15.0f; 
    public float punchRepulseForce = 150f;

     public string oldOrientation = "left";

    void Start()
    {
        PunchCollider = GetComponent<Collider2D>();
        Animator = GetComponent<Animator>();
        soundHandler = GetComponent<SoundHandler>();
        SpriteRenderer = GetComponent<SpriteRenderer>();    
    }
    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.name == "PunchHandler") {
                Vector2 orientation = new Vector2(
                    (this.gameObject.transform.position.x - collision.gameObject.transform.position.x) * 3,
                    (this.gameObject.transform.position.y - collision.gameObject.transform.position.y) * 3
                );
                print("repulseVector " + orientation);
                PlayerMovement.rigidBody.AddForce(orientation * punchRepulseForce, ForceMode2D.Impulse);
                soundHandler.ChangeTheSound(2);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(Player.punchInput) && !isPunching && !PlayerDeath.isDead && !Player.isMakingAnAction) 
        {  
            isPunching = true;
            Player.isMakingAnAction = true;
            Punch();
        } 
    }
    
    void Punch() {
        // print("Punch !");
        soundHandler.ChangeTheSound(Random.Range(0, 2));
        if(
            (PlayerMovement.orientation == "top" && PlayerJump.isJumping) ||
            (PlayerMovement.orientation == "bottom" && !PlayerMovement.isGrounded)) { 
            float acceleration = PlayerMovement.orientation == "bottom" ? -accelerationOnPunch : accelerationOnPunch;
            PlayerMovement.rigidBody.AddForce(new Vector2(PlayerMovement.rigidBody.velocity.x, acceleration), ForceMode2D.Impulse);
        }
        StartCoroutine(PunchActivation());
    }

    IEnumerator PunchActivation()
    {
        PunchCollider.enabled = true;
        if(PlayerMovement.orientation == "left" || PlayerMovement.orientation == "right") {
            Animator.SetTrigger("HorizontalPunch");
        }
        if(PlayerMovement.orientation == "top" || PlayerMovement.orientation == "bottom") {
            Animator.SetTrigger("VerticalPunch");
        }
        if(PlayerMovement.orientation == "left" || PlayerMovement.orientation == "right") {
            this.gameObject.transform.localPosition = new Vector2(PlayerMovement.orientation == "left" ? -0.75f : .75f, 0);
        }
        if(PlayerMovement.orientation == "top" || PlayerMovement.orientation == "bottom") {
            this.gameObject.transform.localPosition = new Vector2(0, PlayerMovement.orientation == "top" ? 1f : -1f);
        }
        SpriteRenderer.flipX = PlayerMovement.orientation == "left";


        yield return new WaitForSeconds(.3f);
        PunchCollider.enabled = false;
        isPunching = false;
        Player.isMakingAnAction = false;
    }

}