using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private SoundHandler soundHandler;

    private List<string> collisionTags =  new List<string> {"Ground", "Player"};
    public bool isDestroyed = false;
    public bool isTaping = false;

    public float arrowSpeed = 0f;

    private int numberOfPunches = 0;

    // Interesting solution to keep track of old velocity before collision
    // private int velocityBeforePhysicsUpdate;
    // void FixedUpdate()
    // {
    //     velocityBeforePhysicsUpdate = rigidbody.velocity;
    // }

    void Start()
    {
        soundHandler = GetComponent<SoundHandler>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision) {

        // bool isSelfArrow = collision.gameObject.tag == "Player" && this.gameObject.name.Contains(collision.gameObject.tag);

        if(collision.gameObject.name.Contains("Arrow")) {
            if(!isTaping) {
               Destruction("tching");
            }
        }
        else if(collision.gameObject.name == "PunchHandler") {
            this.gameObject.name = "Arrow-" + collision.gameObject.transform.parent.name; 
            this.gameObject.layer = collision.gameObject.transform.parent.GetComponent<Player>().index + 8;
            Vector2 orientation = new Vector2(
                (collision.gameObject.transform.position.x - this.gameObject.transform.position.x) * 3,
                (collision.gameObject.transform.position.y - this.gameObject.transform.position.y)
            );
            this.gameObject.transform.eulerAngles = this.gameObject.transform.eulerAngles + 180f * Vector3.up;
            rigidBody.velocity = new Vector2(0,0);
            // rigidBody.AddForce(-orientation, ForceMode2D.Impulse);
            rigidBody.AddForce(-orientation * (arrowSpeed + (numberOfPunches / 2)), ForceMode2D.Impulse);
            numberOfPunches++;
        }
        if(collisionTags.Contains(collision.gameObject.tag)) {
            Destruction("tap");
        }

    }

    void Destruction(string soundName) {
        StartCoroutine(DestructionActivation(soundName));
    }

    IEnumerator DestructionActivation(string soundName)
    {
        if(!isDestroyed) {
            if(soundName=="tap")
                soundHandler.ChangeTheSound(0);
            if(soundName=="tching")
                soundHandler.ChangeTheSound(1);
            rigidBody.velocity = new Vector2(0,0);
            rigidBody.simulated = false;
            boxCollider.enabled = false;
            isDestroyed = true;
        }
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
}
