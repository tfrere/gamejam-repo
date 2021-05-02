using UnityEngine;

public class DownKick : MonoBehaviour
{

    public GameObject kick;
    private Collider2D player;
    // private Rigidbody2D bulletplayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Collider2D>();

    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // print("OnCollisionEnter2D - " + collision.gameObject.tag);
        // Enter -> So if tag is present, that's good
        if (Input.GetKey("s")) {
            if (collision.gameObject.tag == "Player") {
                Debug.Log("DownKick  Player");
            }

            if (collision.gameObject.tag == "Ground") {
                Debug.Log("DownKick Ground");
            }
        }
    }

}
