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
    private SoundHandler soundHandler;
    public ParticleSystem particleSystemComponent;

    void Start()
    {
        player = GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        soundHandler = GetComponent<SoundHandler>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        handleDeath(collision);
    }

    int getKillerIndex(string name)
    {
        return (int)Char.GetNumericValue(name[name.Length - 1]);
    }

    void handleDeath(Collision2D collision)
    {
        bool isSelfPunch = collision.gameObject.transform.parent && collision.gameObject.transform.parent.name == this.gameObject.name;
        bool isSelfArrow = collision.gameObject.name.Contains("Arrow") && collision.gameObject.name.Contains(this.gameObject.name);
        // print("player death collide with -> " + collision.gameObject.name + " with tag : " + collision.gameObject.tag + " from : " + this.gameObject.name);
        // print("self punch " + isSelfPunch);
        // print("self arrow " + isSelfArrow);

        int killerIndex = 0;
        // if it's an arrow
        if (collision.gameObject.name.Contains("Arrow"))
        {
            killerIndex = getKillerIndex(collision.gameObject.name);
        }
        // if it's a sword
        if (collision.gameObject.name.Contains("Punch"))
        {
            killerIndex = getKillerIndex(collision.gameObject.transform.parent.name);
        }
        if (!player.isInvicible)
        {
            if (collision.gameObject.tag == "Death" && (!isSelfPunch && !isSelfArrow))
            {
                GameInfo.playerScores[killerIndex - 1]++;

                var shape = particleSystemComponent.shape;
                Vector3 targetDirection = collision.gameObject.transform.position - this.gameObject.transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 360f, 0.0f);
                // Debug.DrawRay(transform.position, newDirection, Color.red);
                particleSystemComponent.transform.rotation = Quaternion.LookRotation(newDirection * -1);

                Death();
            }
            else if (collision.gameObject.tag == "SelfDeath")
            {
                GameInfo.playerScores[player.index]--;
                Death();
            }
        }
    }

    void Death()
    {
        StartCoroutine(DeathActivation());
    }

    IEnumerator DeathActivation()
    {
        player.isDead = true;
        rigidBody.simulated = false;
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;
        particleSystemComponent.Play();
        soundHandler.ChangeTheSound(UnityEngine.Random.Range(8, 10));
        // Time.timeScale = 0f;
        // Camera shake
        // TO DO : handle this kind of behavior via an event system
        GameObject.Find("Camera").GetComponent<CameraShake>().shakeDuration = 0.15f;
        yield return new WaitForSeconds(1f);
        // Time.timeScale = 0f;
        Destroy(this.gameObject.gameObject);
    }
}
