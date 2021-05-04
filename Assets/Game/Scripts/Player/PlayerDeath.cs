using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDeath : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private SoundHandler soundHandler;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        soundHandler = GetComponent<SoundHandler>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Death") {
            if(this.gameObject.name.Contains("PlayerOne")) {
                GameInfo.PlayerTwoScore++;
            }
            if(this.gameObject.name.Contains("PlayerTwo")) {
                GameInfo.PlayerOneScore++;
            }
            Death();
        }
        if(collision.gameObject.tag == "SelfDeath") {
            if(this.gameObject.name.Contains("PlayerOne")) {
                GameInfo.PlayerOneScore--;
            }
            if(this.gameObject.name.Contains("PlayerTwo")) {
                GameInfo.PlayerTwoScore--;
            }
            Death();
        }
    }

    void Death() {
        StartCoroutine(DeathActivation());
    }

    IEnumerator DeathActivation()
    {
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;
        soundHandler.ChangeTheSound(Random.Range(3, 6));
        print("shouldberenderedd");
        yield return new WaitForSeconds(.5f);
        Destroy(this.gameObject.gameObject);
    }
}
