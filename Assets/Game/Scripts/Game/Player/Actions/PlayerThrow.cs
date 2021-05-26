using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// TO REFACTOR 
public class PlayerThrow : MonoBehaviour
{
    public Player player;
    private SoundHandler soundHandler;
    private string throwedObjectName;

    void Start()
    {
        soundHandler = GetComponent<SoundHandler>();
        throwedObjectName = player.throwObject.name + "-" + this.gameObject.transform.parent.name;
    }

    public void ThrowInputAction(InputAction.CallbackContext context)
    {
        if (context.performed && !player.isThrowing && !player.isDead && !player.isMakingAnAction)
        {
            Throw();
        }
    }

    void Throw()
    {

        if (GameInfo.playerArrows[player.index] > 0)
        {
            GameInfo.playerArrows[player.index]--;
            ThrowHandle();
        }
        else
        {
            soundHandler.ChangeTheSound(6);
            player.isThrowing = false;
        }
    }

    void ThrowHandle()
    {
        StartCoroutine(ThrowActivation());
    }

    IEnumerator ThrowActivation()
    {
        player.isThrowing = true;
        player.isMakingAnAction = true;

        string normalizedOrientation = player.currentOrientation != "none" ? player.currentOrientation : player.oldHorizontalOrientation;

        if (normalizedOrientation == "left" || normalizedOrientation == "right")
        {
            float positionOffset = normalizedOrientation == "left" ? -1f : 1f;
            Vector3 arrowVector = normalizedOrientation == "left" ? Vector3.left : Vector3.right;
            Vector3 arrowPosition = new Vector3(this.transform.position.x + positionOffset, this.transform.position.y, this.transform.position.z);
            Quaternion quaternion = normalizedOrientation == "left" ? Quaternion.AngleAxis(180, Vector3.forward) : Quaternion.AngleAxis(0, Vector3.forward);
            InstanciateThrowedObject(arrowVector, arrowPosition, quaternion);
        }
        if (normalizedOrientation == "up" || normalizedOrientation == "down")
        {
            float positionOffset = normalizedOrientation == "up" ? 1f : -1f;
            Vector3 arrowVector = normalizedOrientation == "up" ? Vector3.up : Vector3.down;
            Vector3 arrowPosition = new Vector3(this.transform.position.x, this.transform.position.y + positionOffset, this.transform.position.z);
            Quaternion quaternion = normalizedOrientation == "up" ? Quaternion.AngleAxis(90, Vector3.forward) : Quaternion.AngleAxis(-90, Vector3.forward);
            InstanciateThrowedObject(arrowVector, arrowPosition, quaternion);
        }
        yield return new WaitForSeconds(.3f);
        player.isThrowing = false;
        player.isMakingAnAction = false;
    }

    void InstanciateThrowedObject(Vector3 arrowVector, Vector3 arrowPosition, Quaternion quaternion)
    {
        GameObject newThrowedObject = Instantiate(player.throwObject, arrowPosition, quaternion);
        newThrowedObject.name = throwedObjectName;
        // change for the player layer
        newThrowedObject.layer = player.index + 8;
        //
        newThrowedObject.GetComponent<Rigidbody2D>().AddForce(arrowVector * player.throwSpeed, ForceMode2D.Impulse);
        newThrowedObject.GetComponent<Arrow>().arrowSpeed = player.throwSpeed;
        soundHandler.ChangeTheSound(Random.Range(0, 5));
    }

}
