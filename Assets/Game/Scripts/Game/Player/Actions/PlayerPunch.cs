using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPunch : MonoBehaviour
{
     public Player player;
     public PlayerMovement playerMovement;
     public PlayerOrientation playerOrientation;
     public PlayerJump playerJump;
     public PlayerDeath playerDeath;
     private Collider2D punchCollider;
     private SpriteRenderer spriteRenderer;
     private Animator animator;
     private SoundHandler soundHandler;

    public bool isPunching;
    public float accelerationOnPunch = 15.0f; 
    public float punchRepulseForce = 150f;

    void Start()
    {
        punchCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        soundHandler = GetComponent<SoundHandler>();
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }
    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.name == "PunchHandler") {
                Vector2 orientation = new Vector2(
                    (this.gameObject.transform.position.x - collision.gameObject.transform.position.x) * 3,
                    (this.gameObject.transform.position.y - collision.gameObject.transform.position.y) * 3
                );
                print("repulseVector " + orientation);
                playerMovement.rigidBody.AddForce(orientation * punchRepulseForce, ForceMode2D.Impulse);
                soundHandler.ChangeTheSound(2);
        }
    }
    
    public void PunchInputAction(InputAction.CallbackContext context)
    {
        if(context.performed && !isPunching && !playerDeath.isDead && !player.isMakingAnAction) {
            print("Punch!");
            Punch();
        }
    }


    void Punch() {
        // print("Punch !");
        isPunching = true;
        player.isMakingAnAction = true;
        soundHandler.ChangeTheSound(Random.Range(0, 2));
        // handle move punch super move
        if(
            (playerOrientation.currentOrientation == "up" && playerJump.isJumping) ||
            (playerOrientation.currentOrientation == "down" && !playerMovement.isGrounded)) { 
            float acceleration = playerOrientation.currentOrientation == "down" ? -accelerationOnPunch : accelerationOnPunch;
            playerMovement.rigidBody.AddForce(new Vector2(playerMovement.rigidBody.velocity.x, acceleration), ForceMode2D.Impulse);
        }
        StartCoroutine(PunchActivation());
    }

    IEnumerator PunchActivation()
    {
        punchCollider.enabled = true;
        if(playerOrientation.currentOrientation == "left" || playerOrientation.currentOrientation == "right") {
            animator.SetTrigger("HorizontalPunch");
            this.gameObject.transform.localPosition = new Vector2(playerOrientation.currentOrientation == "left" ? -0.75f : .75f, 0);
        }
        else if(playerOrientation.currentOrientation == "up" || playerOrientation.currentOrientation == "down") {
            animator.SetTrigger("VerticalPunch");
            this.gameObject.transform.localPosition = new Vector2(0, playerOrientation.currentOrientation == "up" ? 1f : -1f);
        }
        else if(playerOrientation.currentOrientation == "none" && (playerOrientation.oldHorizontalOrientation == "left" || playerOrientation.oldHorizontalOrientation == "right")) {
            animator.SetTrigger("HorizontalPunch");
            this.gameObject.transform.localPosition = new Vector2(playerOrientation.oldHorizontalOrientation == "left" ? -0.75f : .75f, 0);
        }

        spriteRenderer.flipX = playerOrientation.currentOrientation == "none" ? playerOrientation.oldHorizontalOrientation == "left" : playerOrientation.currentOrientation == "left";
        spriteRenderer.flipY = playerOrientation.currentOrientation == "down";

        yield return new WaitForSeconds(.3f);
        punchCollider.enabled = false;
        isPunching = false;
        player.isMakingAnAction = false;
    }

}
