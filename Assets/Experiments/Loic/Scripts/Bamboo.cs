using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bamboo : MonoBehaviour
{
    public GameObject bambooChunk;
    public Sprite bambooChunkBody;
    public Sprite bambooChunkTop;
    public int size = 1;

    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
        for(int i = 1; i <= size; i++)
        {
            GameObject bambooChunkClone = Instantiate(bambooChunk, new Vector3(this.gameObject.transform.position.x, i - 1 + this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
            bambooChunkClone.GetComponent<SpriteRenderer>().sprite = bambooChunkBody;
            if (i == size) {
                bambooChunkClone.GetComponent<SpriteRenderer>().sprite = bambooChunkTop;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
