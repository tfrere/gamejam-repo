using UnityEngine;
using System.Collections;

public class PunchAction : MonoBehaviour
{


    // animator
    // public Animator animator;

    // spriterenderer
    // public SpriteRenderer spriteRenderer;

    
    // Destroy(this.gameObject)
    // Start is called before the first frame update
    void Start()
    {
        launchPunch();
    }

    void Update() 
    {
        this.transform.position = new Vector2(this.transform.position.x + this.transform.localScale.x * 0.5f, this.transform.position.y); 
    }
    


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("DownKick  Player");
            Destroy(collision.gameObject, 0f);
            Destroy(this.gameObject, 0f);
        }

        if (collision.gameObject.tag == "Ground") {
            Debug.Log("DownKick Ground");
        }
    }

    IEnumerator launchPunch() 
    {
        yield return new WaitForSeconds(.5f);
        Destroy(this.gameObject);
    }

}
