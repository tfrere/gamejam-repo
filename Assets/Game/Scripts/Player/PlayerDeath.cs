using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    private SoundHandler soundHandler;

    // Start is called before the first frame update
    void Start()
    {
        soundHandler = GetComponent<SoundHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name);
        print(this.gameObject.name);
        if(collision.gameObject.name == "Player2(Clone)" && this.gameObject.name == "PunchCollider") {
            Destroy(collision.gameObject);
            GameInfo.PlayerOneScore++;
            soundHandler.ChangeTheSound(Random.Range(3, 6));
        }
        if(collision.gameObject.name == "Player(Clone)" && this.gameObject.name == "PunchCollider2") {
            Destroy(collision.gameObject);
            GameInfo.PlayerTwoScore++;
            soundHandler.ChangeTheSound(Random.Range(3, 6));
        }
        // // print("OnCollisionEnter2D - " + collision.gameObject.tag);
        // isGrounded = collision.gameObject.tag == "Ground";
        // isOnWall = collision.gameObject.tag == "Wall";
    }
    
}
