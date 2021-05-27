using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// TO REFACTOR 
public class PlayerPunch : MonoBehaviour
{
    public Player player;
    private Collider2D punchCollider;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private SoundHandler soundHandler;

    void Start()
    {
        punchCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        soundHandler = GetComponent<SoundHandler>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PunchHandler")
        {
            print("Tching !");
            MakeTemporarlyInvincible();
            Vector2 orientation = new Vector2(
                (this.gameObject.transform.position.x - collision.gameObject.transform.position.x),
                (this.gameObject.transform.position.y - collision.gameObject.transform.position.y)
            );
            player.updateVelocity(orientation * player.punchRepulseForce);
            soundHandler.ChangeTheSound(2);
        }
    }

    public void PunchInputAction(InputAction.CallbackContext context)
    {
        if (context.performed && !player.isDead && !player.isPunching && !player.isMakingAnAction)
        {
            Punch();
        }
    }

    IEnumerator MakeTemporarlyInvincible()
    {
        player.isInvicible = true;
        yield return new WaitForSeconds(.1f);
        player.isInvicible = false;
    }


    void Punch()
    {
        player.isPunching = true;
        player.isMakingAnAction = true;
        soundHandler.ChangeTheSound(Random.Range(0, 2));
        // handle punch super move
        if (
            (player.currentOrientation == "up" && player.isJumping) ||
            (player.currentOrientation == "down" && !player.isGrounded))
        {
            float acceleration = player.currentOrientation == "down" ? -player.accelerationOnPunch : player.accelerationOnPunch;
            // player.AddForce(new Vector2(player.rb.velocity.x, acceleration), ForceMode2D.Impulse);
            player.updateVelocity(new Vector2(player.rb.velocity.x, acceleration));
        }
        StartCoroutine(PunchActivation());
    }

    IEnumerator PunchActivation()
    {
        punchCollider.enabled = true;

        // punch left and right
        if (player.currentOrientation == "left" || player.currentOrientation == "right")
        {
            animator.SetTrigger("HorizontalPunch");
            StartCoroutine(LerpPosition(new Vector2(player.currentOrientation == "left" ? -0.75f : .75f, 0), player.punchLerpTime));
            spriteRenderer.flipX = player.currentOrientation == "left";
        }
        // punch up and down
        else if (player.currentOrientation == "up" || player.currentOrientation == "down")
        {
            animator.SetTrigger("VerticalPunch");
            StartCoroutine(LerpPosition(new Vector2(0, player.currentOrientation == "up" ? 1f : -1f), player.punchLerpTime));
            spriteRenderer.flipY = player.currentOrientation == "down";
        }
        // without current orientation lets punch in the last horizontal directiion
        else if (player.currentOrientation == "none" && (player.oldHorizontalOrientation == "left" || player.oldHorizontalOrientation == "right"))
        {
            animator.SetTrigger("HorizontalPunch");
            StartCoroutine(LerpPosition(new Vector2(player.oldHorizontalOrientation == "left" ? -0.75f : .75f, 0), player.punchLerpTime));
            spriteRenderer.flipX = player.oldHorizontalOrientation == "left";
        }

        yield return new WaitForSeconds(.3f);
        punchCollider.enabled = false;
        player.isPunching = false;
        player.isMakingAnAction = false;
    }

    IEnumerator LerpPosition(Vector2 targetPosition, float duration)
    {
        float time = 0;
        Vector2 startPosition = new Vector2(0, 0);

        while (time < duration)
        {
            this.gameObject.transform.localPosition = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        this.gameObject.transform.localPosition = targetPosition;
    }

}
