using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
     public PlayerMovement PlayerMovement;
     public PlayerDeath PlayerDeath;
     public GameObject Arrow;
     public float ArrowSpeed = 50;
     private SpriteRenderer ArrowSpriteRenderer;
     private SoundHandler SoundHandler;

     private bool isThrowing;

     public string throwKey = "q";

    private string arrowName;

    void Start()
    {
        SoundHandler = GetComponent<SoundHandler>();
        ArrowSpriteRenderer = Arrow.GetComponent<SpriteRenderer>();
        arrowName = Arrow.name + "-" + this.gameObject.transform.parent.name;
    }

    void Update()
    {
        if (Input.GetKeyDown(throwKey) && !isThrowing && !PlayerDeath.isDead)
        {  
            isThrowing = true;
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
            SoundHandler.ChangeTheSound(6);
            isThrowing = false;
        }

    }

    void ThrowHandle() {
        if(PlayerMovement.orientation == "left" || PlayerMovement.orientation == "right") {
            float positionOffset = PlayerMovement.orientation == "left" ? -1f : 1f;
            Vector3 arrowVector = PlayerMovement.orientation == "left" ? Vector3.left : Vector3.right;
            Vector3 arrowPosition = new Vector3(this.transform.position.x + positionOffset, this.transform.position.y, this.transform.position.z);
            Quaternion quaternion = PlayerMovement.orientation == "left" ? Quaternion.AngleAxis(180, Vector3.forward): Quaternion.AngleAxis(0, Vector3.forward);
            GameObject newArrow = Instantiate(Arrow, arrowPosition, quaternion);
            newArrow.name = arrowName;
            newArrow.GetComponent<Rigidbody2D>().AddForce(arrowVector * ArrowSpeed, ForceMode2D.Impulse);
        }
        if(PlayerMovement.orientation == "top" || PlayerMovement.orientation == "bottom") {
            float positionOffset = PlayerMovement.orientation == "top" ? 1f : -1.5f;
            Vector3 arrowVector = PlayerMovement.orientation == "top" ? Vector3.up : Vector3.back;
            Vector3 arrowPosition = new Vector3(this.transform.position.x, this.transform.position.y + positionOffset, this.transform.position.z);
            Quaternion quaternion = PlayerMovement.orientation == "top" ? Quaternion.AngleAxis(90, Vector3.forward): Quaternion.AngleAxis(-90, Vector3.forward);
            GameObject newArrow = Instantiate(Arrow, arrowPosition, quaternion);
            newArrow.name = arrowName;
            newArrow.GetComponent<Rigidbody2D>().AddForce(arrowVector * ArrowSpeed, ForceMode2D.Impulse);
        }
        SoundHandler.ChangeTheSound(Random.Range(0, 5));
        StartCoroutine(ThrowActivation());

    }

    IEnumerator ThrowActivation()
    {
        yield return new WaitForSeconds(.3f);
        isThrowing = false;
    }
}
