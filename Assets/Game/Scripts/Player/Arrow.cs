using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private SoundHandler soundHandler;

    private List<string> collisionTags =  new List<string> {"Ground", "Borders", "Wall"};
    public bool isDestroyed = false;
    public bool isTaping = false;

    void Start()
    {
        soundHandler = GetComponent<SoundHandler>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision) {

        bool isSelfArrow = collision.gameObject.tag == "Player" && this.gameObject.name.Contains(collision.gameObject.tag);
        
        print("arrow collision");
        // if pickable
        if(isDestroyed && collision.gameObject.name.Contains("Player")) { 
            print("pickable");
            if(collision.gameObject.name == "PlayerOne") {
                GameInfo.PlayerOneArrows++;
            }
            else if(collision.gameObject.name == "PlayerTwo") {
                GameInfo.PlayerTwoArrows++;
            }
            Destroy(this.gameObject);
        }
        else {
            // if arrow collision
            if(collision.gameObject.name.Contains("Arrow")) { 
                if(!isTaping) {
                    TapSound();
                }
            }

            // if returned
            if(collision.gameObject.name == "PunchHandler") {
                this.gameObject.name = "Arrow-" + collision.gameObject.transform.parent.name; 
                Vector2 orientation = new Vector2(
                (collision.gameObject.transform.position.x - this.gameObject.transform.position.x) * 3,
                (collision.gameObject.transform.position.y - this.gameObject.transform.position.y)
                );
                this.gameObject.transform.eulerAngles = this.gameObject.transform.eulerAngles + 180f * Vector3.up;
                rigidBody.velocity = new Vector2(0,0);
                rigidBody.AddForce(-orientation * 10f, ForceMode2D.Impulse);
            }
            else if(!isSelfArrow && collisionTags.Contains(collision.gameObject.tag)) {
                Destruction();
            }
        }

    }

    void TapSound() {
        StartCoroutine(TapSoundActivation());
    }

    IEnumerator TapSoundActivation()
    {
        isTaping = true;
        soundHandler.ChangeTheSound(1);
        print("tapp");
        yield return new WaitForSeconds(0.3f);
        isTaping = false;
    }

    void Destruction() {
        StartCoroutine(DestructionActivation());
    }

    IEnumerator DestructionActivation()
    {
        if(!isDestroyed) {
            soundHandler.ChangeTheSound(0);
            rigidBody.velocity = new Vector2(0,0);
            // rigidBody.simulated = false;
            // boxCollider.enabled = false;
            isDestroyed = true;
        }
        yield return new WaitForSeconds(0.3f);
        // Destroy(this.gameObject);
        this.gameObject.name = "Arrow-pickable"; 
    }
}
