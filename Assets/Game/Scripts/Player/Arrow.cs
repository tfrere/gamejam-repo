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
        if(collision.gameObject.name.Contains("Arrow")) { 
            // dont work 
            if(!isTaping) {
                TapSound();
            }
        }
        bool isSelfArrow = collision.gameObject.tag == "Player" && this.gameObject.name.Contains(collision.gameObject.tag);

        if(!isSelfArrow && collisionTags.Contains(collision.gameObject.tag)) {
            Destruction();
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
            // print("arrow will destroy");
            // this.gameObject.GetComponent<AudioSource>().Play();
            rigidBody.simulated = false;
            boxCollider.enabled = false;
            // spriteRenderer.enabled = false;
            isDestroyed = true;
        }
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
}
