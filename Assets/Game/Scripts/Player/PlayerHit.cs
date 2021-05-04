using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
     public PlayerMovement PlayerMovement;
     private Collider2D PunchCollider;
     private SpriteRenderer SpriteRenderer;
     private Animator Animator;
     private SoundHandler soundHandler;

     private bool isPunching;

     public string punchKey = "e";

    // Start is called before the first frame update
    void Start()
    {
        PunchCollider = GetComponent<Collider2D>();
        Animator = GetComponent<Animator>();
        soundHandler = GetComponent<SoundHandler>();
        SpriteRenderer = GetComponent<SpriteRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.localPosition = new Vector2(PlayerMovement.facingLeft ? -0.17f : .25f, 0);
        // PunchCollider.offset = new Vector2(PlayerMovement.facingLeft ? -1.2f : -.2f, 0);
        SpriteRenderer.flipX = PlayerMovement.facingLeft;

        if (Input.GetKeyDown(punchKey) && !isPunching)
        {  
            Punch();
            isPunching = true;
        }
    }
    
    void Punch() {
        // print("Punch !");
        Animator.SetTrigger("isPunching");
        soundHandler.ChangeTheSound(Random.Range(0, 3));

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
