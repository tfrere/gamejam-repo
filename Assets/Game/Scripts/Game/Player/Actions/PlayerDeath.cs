using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDeath : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private ParticleSystem particleSystem;

    void Start()
    {
        player = GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        handleDeath(collision);
    }

    int getKillerIndex(string name) {
        return (int)Char.GetNumericValue(name[name.Length - 1]);
    }

    void handleDeath(Collision2D collision) {
        // print("player death collide with -> " + collision.gameObject.name + " with tag : " + collision.gameObject.tag + " from : " + this.gameObject.name);
        bool isSelfPunch = collision.gameObject.transform.parent && collision.gameObject.transform.parent.name == this.gameObject.name;
        bool isSelfArrow = collision.gameObject.name.Contains("Arrow") && collision.gameObject.name.Contains(this.gameObject.name);

        print("self punch " + isSelfPunch);
        print("self arrow " + isSelfArrow);

        int killerIndex = 0;
        // if it's an arrow
        if(collision.gameObject.name.Contains("Arrow")) {
            killerIndex = getKillerIndex(collision.gameObject.name);
        }
        // if it's a sword
        if(collision.gameObject.name.Contains("Punch")) {
            killerIndex = getKillerIndex(collision.gameObject.transform.parent.name);
        }

        // print(killerIndex);
        // print(gameObject.tag);

        if(!player.isInvicible) {
            if( collision.gameObject.tag == "Death" && (!isSelfPunch && !isSelfArrow)) {
                // if(this.gameObject.name.Contains("Player")) {
                    GameInfo.playerScores[killerIndex - 1]++;
                // }
                print("hastodeath");
                Death();
            }
            else if(collision.gameObject.tag == "SelfDeath") {
                GameInfo.playerScores[player.index]--;
                Death();
            }
        }
    }

    void Death() {
        StartCoroutine(DeathActivation());
    }

    IEnumerator DeathActivation()
    {
        if(!player.isDead) {
            rigidBody.simulated = false;
            boxCollider.enabled = false;
            spriteRenderer.enabled = false;
            player.isDead = true;
            particleSystem.Play();
            // Camera shake and slow time
            // TO DO : handle this kind of behavior via an event system
            GameObject.Find("Camera").GetComponent<CameraShake>().shakeDuration = 0.3f;
            Time.timeScale = 0.5f;
        }
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject.gameObject);
        Time.timeScale = 1;
    }
}
