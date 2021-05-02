using UnityEngine;
using System.Collections;

public class PunchAction : MonoBehaviour
{


    // animator
    // public Animator animator;

    // spriterenderer
    // public SpriteRenderer spriteRenderer;

    private float direction = 1f;
    // Destroy(this.gameObject)
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"SCALE {this.transform.localScale.x}");
        launchPunch();
    }

    void Update() 
    {
        this.transform.position = new Vector2(this.transform.position.x + direction * 0.5f, this.transform.position.y); 
    }
    
    void SetDirection(float _direction) 
    {
        Debug.Log($"SCALE {_direction}");
        this.direction = _direction;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("DownKick  Player");
            Destroy(collision.gameObject, 0f);
            Destroy(this.gameObject, 0f);
        }

        if (collision.gameObject.tag == "Wall") {
            Debug.Log("DownKick Wall");
            Destroy(this.gameObject, 0f);
        }
    }

    IEnumerator launchPunch() 
    {
        yield return new WaitForSeconds(.5f);
        Destroy(this.gameObject);
    }

}
