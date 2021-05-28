using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    private SoundHandler soundHandler;

    private void Start()
    {
        soundHandler = GetComponent<SoundHandler>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // print(collision.gameObject.name + "   " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Death"))
        {
            soundHandler.ChangeTheSound(0);
            Destroy(this.gameObject);
        }

    }

}