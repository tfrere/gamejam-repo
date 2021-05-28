using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class DestructableTile : MonoBehaviour
{
    private Tilemap tilemap;
    public ParticleSystem particleSystemComponent;
    private SoundHandler soundHandler;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        soundHandler = GetComponent<SoundHandler>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name + "   " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Death"))
        {
            print("i should die");
            print(collision.contacts.Length);
            soundHandler.ChangeTheSound(0);
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                print(hitPosition);
                print(tilemap.WorldToCell(hitPosition));
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            }
        }
    }

}