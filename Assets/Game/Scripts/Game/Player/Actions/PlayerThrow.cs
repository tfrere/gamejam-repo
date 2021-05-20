using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrow : MonoBehaviour
{
    public Player player;
     public PlayerOrientation playerOrientation;
     public PlayerDeath playerDeath;
     public GameObject arrow;
     public float ArrowSpeed = 50;
     private SoundHandler soundHandler;

     private bool isThrowing;

    private string arrowName;

    void Start()
    {
        soundHandler = GetComponent<SoundHandler>();
        arrowName = arrow.name + "-" + this.gameObject.transform.parent.name;
    }

    public void ThrowInputAction(InputAction.CallbackContext context)
    {
        if(context.performed && !isThrowing && !playerDeath.isDead && !player.isMakingAnAction) {
            print("Throw!");
            Throw();
        }
    }

    void Throw() {
        if(this.gameObject.transform.parent.gameObject.name == "PlayerOne" && GameInfo.PlayerOneArrows > 0) {
            GameInfo.PlayerOneArrows--;
            ThrowHandle();
        }
        else if(this.gameObject.transform.parent.gameObject.name == "PlayerTwo" && GameInfo.PlayerTwoArrows > 0) {
            GameInfo.PlayerTwoArrows--;
            ThrowHandle();
        }
        else {
            soundHandler.ChangeTheSound(6);
            isThrowing = false;
        }

    }

    void ThrowHandle() {
        StartCoroutine(ThrowActivation());
    }

    IEnumerator ThrowActivation()
    {
        isThrowing = true;
        player.isMakingAnAction = true;
        if(playerOrientation.currentOrientation == "left" || playerOrientation.currentOrientation == "right") {
            float positionOffset = playerOrientation.currentOrientation == "left" ? -1f : 1f;
            Vector3 arrowVector = playerOrientation.currentOrientation == "left" ? Vector3.left : Vector3.right;
            Vector3 arrowPosition = new Vector3(this.transform.position.x + positionOffset, this.transform.position.y, this.transform.position.z);
            Quaternion quaternion = playerOrientation.currentOrientation == "left" ? Quaternion.AngleAxis(180, Vector3.forward): Quaternion.AngleAxis(0, Vector3.forward);
            GameObject newArrow = Instantiate(arrow, arrowPosition, quaternion);
            newArrow.name = arrowName;
            newArrow.GetComponent<Rigidbody2D>().AddForce(arrowVector * ArrowSpeed, ForceMode2D.Impulse);
        }
        if(playerOrientation.currentOrientation == "up" || playerOrientation.currentOrientation == "down") {
            float positionOffset = playerOrientation.currentOrientation == "up" ? 1f : -1.5f;
            Vector3 arrowVector = playerOrientation.currentOrientation == "up" ? Vector3.up : Vector3.back;
            Vector3 arrowPosition = new Vector3(this.transform.position.x, this.transform.position.y + positionOffset, this.transform.position.z);
            Quaternion quaternion = playerOrientation.currentOrientation == "up" ? Quaternion.AngleAxis(90, Vector3.forward): Quaternion.AngleAxis(-90, Vector3.forward);
            GameObject newArrow = Instantiate(arrow, arrowPosition, quaternion);
            newArrow.name = arrowName;
            newArrow.GetComponent<Rigidbody2D>().AddForce(arrowVector * ArrowSpeed, ForceMode2D.Impulse);
        }
        soundHandler.ChangeTheSound(Random.Range(0, 5));
        yield return new WaitForSeconds(.3f);
        isThrowing = false;
        player.isMakingAnAction = false;
    }
}
