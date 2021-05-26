using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooChunk : MonoBehaviour
{
    public ParticleSystem particleSystemComponent;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private SoundHandler soundHandler;
    private bool isDetroyed;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        soundHandler = GetComponent<SoundHandler>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.tag);
        if (collision.gameObject.tag == "Death")
        {
            particleSystemComponent.Play();
            DestroyBambooChunk();
        }
    }

    void DestroyBambooChunk()
    {
        StartCoroutine(DestroyActivation());
    }
    IEnumerator DestroyActivation()
    {
        if (!isDetroyed)
        {
            boxCollider.enabled = false;
            spriteRenderer.enabled = false;
            soundHandler.ChangeTheSound(0);
            particleSystemComponent.Play();
            isDetroyed = true;
        }
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }

}
