using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
     public PlayerMovement PlayerMovement;
     public PlayerJump PlayerJump;
     public PlayerDeath PlayerDeath;
     private Collider2D PunchCollider;
     private SpriteRenderer SpriteRenderer;
     private Animator Animator;
     private SoundHandler soundHandler;

     private bool isPunching;
    public float accelerationOnPunch = 15.0f; 
    public float punchRepulseForce = 15f;

     public string punchKey = "e";

    void Start()
    {
        PunchCollider = GetComponent<Collider2D>();
        Animator = GetComponent<Animator>();
        soundHandler = GetComponent<SoundHandler>();
        SpriteRenderer = GetComponent<SpriteRenderer>();    
    }

    // Make a collision to perrform this effect ! 

    
    // void OnTriggerEnter2D(Collider2D collider)
    // {
    //     float orientation = PlayerMovement.orientation == "left" ? 1: -1;
    //     Vector2 repulseVector = new Vector2(punchRepulseForce * orientation, 0); 
    //     PlayerMovement.rigidBody.AddForce(repulseVector, ForceMode2D.Impulse);
    //     soundHandler.ChangeTheSound(2);

    //     // Collider2D[] contacts = new Collider2D[1];
    //     // collider.GetContacts(contacts);
    //     // // print(contacts);
    //     // // print(this.gameObject.transform.position.x);
    //     // // print(contacts[0].gameObject.name);
    //     // print(this.gameObject.transform.position.x);
    //     // print(contacts[0].gameObject.transform.position.x);
    //     // print(this.gameObject.transform.position.x - contacts[0].gameObject.transform.position.x);
    //     // string orientation = this.gameObject.transform.position.x - contacts[0].gameObject.transform.position.x > 0 ? "left" : "right";
    //     // print(orientation);
    //     // Collider2D contact = collider.GetContacts(new Collider2D[1]);
    //     // if(contact.gameObject.name == "PlayerPunch") {
    //     //     print("sthing");
    //     // }
    // }

    void Update()
    {
        if(PlayerMovement.orientation == "left" || PlayerMovement.orientation == "right") {
            this.gameObject.transform.localPosition = new Vector2(PlayerMovement.orientation == "left" ? -0.55f : .55f, 0);

        }
        if(PlayerMovement.orientation == "top" || PlayerMovement.orientation == "bottom") {
            this.gameObject.transform.localPosition = new Vector2(0, PlayerMovement.orientation == "top" ? 0.75f : -0.75f);
        }
        SpriteRenderer.flipX = PlayerMovement.orientation == "left";

        if (Input.GetKeyDown(punchKey) && !isPunching && !PlayerDeath.isDead) 
        {  
            Punch();
            isPunching = true;
        } 
    }
    
    void Punch() {
        // print("Punch !");
        Animator.SetTrigger("isPunching");
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
        yield return new WaitForSeconds(.3f);
        PunchCollider.enabled = false;
        isPunching = false;
    }
}
