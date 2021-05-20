using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDeath : MonoBehaviour
{

    private Player player;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private SoundHandler soundHandler;
    public ParticleSystem particleSystem;
    private Animator animator;

    public bool isDead = false;

    void Start()
    {
        player = GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        soundHandler = GetComponent<SoundHandler>();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        handleDeath(collision.gameObject);
    }

    void handleDeath(GameObject gameObject) {
        // print("player death collide with -> " + collision.gameObject.name + " with tag : " + collision.gameObject.tag + " from : " + this.gameObject.name);
        bool isSelfPunch = gameObject.transform.parent && gameObject.transform.parent.name == this.gameObject.name;
        bool isSelfArrow = gameObject.name.Contains("Arrow") && gameObject.name.Contains(this.gameObject.name);
        bool isPickableArrow = gameObject.name == "Arrow-pickable";

        // print("isSelfPunch " + isSelfPunch);
        // print("isSelfArrow " + isSelfArrow);
        // print("tag " + gameObject.tag);
        if(!player.isInvicible) {
            if( gameObject.tag == "Death"   
                && (!isSelfPunch && !isSelfArrow && !isPickableArrow)) {
                if(this.gameObject.name.Contains("PlayerOne")) {
                    GameInfo.PlayerTwoScore++;
                }
                else if(this.gameObject.name.Contains("PlayerTwo")) {
                    GameInfo.PlayerOneScore++;
                }
                Death();
            }
            else if(gameObject.tag == "SelfDeath") {
                if(this.gameObject.name.Contains("PlayerOne")) {
                    GameInfo.PlayerOneScore--;
                }
                else if(this.gameObject.name.Contains("PlayerTwo")) {
                    GameInfo.PlayerTwoScore--;
                }
                Death();
            }
        }
    }

    void Death() {
        StartCoroutine(DeathActivation());
    }

    IEnumerator DeathActivation()
    {
        if(!isDead) {
            rigidBody.simulated = false;
            boxCollider.enabled = false;
            spriteRenderer.enabled = false;
            // animator.SetBool("isDead", true);
            soundHandler.ChangeTheSound(Random.Range(8, 10));
            particleSystem.Play();
            isDead = true;
            // Camera shake 
            // TO DO : handle this kind of behavior via an event system
            GameObject.Find("Camera").GetComponent<CameraShake>().shakeDuration = 0.3f;
            Time.timeScale = 0.5f;
        }
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject.gameObject);
        Time.timeScale = 1;
    }
}
