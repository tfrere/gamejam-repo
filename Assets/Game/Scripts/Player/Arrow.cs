using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "InvisibleBorders" || collision.gameObject.tag == "Wall") {
            Destroy(gameObject);
        }
    }
}
