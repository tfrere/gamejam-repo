using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
     public PlayerMovement PlayerMovement;
     public GameObject Arrow;
     public float ArrowSpeed = 50;
     private SpriteRenderer ArrowSpriteRenderer;
     private SoundHandler SoundHandler;

     private bool isThrowing;

     public string throwKey = "q";

    void Start()
    {
        SoundHandler = GetComponent<SoundHandler>();
        ArrowSpriteRenderer = Arrow.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(throwKey) && !isThrowing)
        {  
            Throw();
            isThrowing = true;
        }
    }
    void Throw() {
        SoundHandler.ChangeTheSound(Random.Range(0, 5));
        float positionOffset = PlayerMovement.facingLeft ? -2 : 2;
        Vector3 arrowVector = PlayerMovement.facingLeft ? Vector3.left : Vector3.right;
        Vector3 arrowPosition = new Vector3(this.transform.position.x + positionOffset, this.transform.position.y, this.transform.position.z);
        GameObject newArrow = Instantiate(Arrow, arrowPosition, Quaternion.identity);
        newArrow.GetComponent<SpriteRenderer>().flipX = PlayerMovement.facingLeft;
        newArrow.GetComponent<Rigidbody2D>().AddForce(arrowVector * ArrowSpeed, ForceMode2D.Impulse);
        StartCoroutine(ThrowActivation());
    }

    IEnumerator ThrowActivation()
    {
        yield return new WaitForSeconds(.3f);
        isThrowing = false;
    }
}
