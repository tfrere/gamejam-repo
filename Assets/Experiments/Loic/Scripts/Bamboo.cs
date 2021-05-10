using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bamboo : MonoBehaviour
{
    public GameObject bambooChunk;

    public Sprite bambooChunkBody;
    public Sprite bambooChunkTop;

    [Range(1, 8)]
    public int size = 1;
    
    public float minGrowTime = 2.0f;
    public float maxGrowTime = 20.0f;

    private List<string> bambooChunks = new List<string>();
   
    private float bambooPositionX;
    private float bambooPositionY;
    private float bambooPositionZ;

    void Start()
    {
        // Get bamboo position
        bambooPositionX = this.gameObject.transform.position.x;
        bambooPositionY = this.gameObject.transform.position.y;
        bambooPositionZ = this.gameObject.transform.position.z;
        // Remove bamboo sprite
        this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
        // Instantiate a fixed number of chunks at start depending of the public size variable
        for(int i = 1; i <= size; i++)
        {
            // Instantiate new bamboo chunk at the top of the last chunk
            Vector3 newBambooChunkOffset = new Vector3(bambooPositionX, i - 1 + bambooPositionY, bambooPositionZ);
            GameObject newBambooChunk = Instantiate(bambooChunk, newBambooChunkOffset, Quaternion.identity);
            newBambooChunk.GetComponent<SpriteRenderer>().sprite = bambooChunkBody;
            // Add new bamboo chunk ID to bamboo chunks list
            bambooChunks.Add(newBambooChunk.GetInstanceID().ToString());
            newBambooChunk.name = newBambooChunk.GetInstanceID().ToString();
            // Set the bamboo top sprite
            if (i == size) {
                newBambooChunk.GetComponent<SpriteRenderer>().sprite = bambooChunkTop;                
            }
        }
        // Create a new bamboo chunk each 2 to 20 seconds
        InvokeRepeating("BambooChunkGrow", Random.Range(minGrowTime, maxGrowTime), Random.Range(minGrowTime, maxGrowTime));
    }

    void BambooChunkGrow()
    {
        if (size < 8) {
            // Replace last bamboo chunk sprite
            GameObject lastBambooChunk = GameObject.Find(bambooChunks[size - 1]);
            lastBambooChunk.GetComponent<SpriteRenderer>().sprite = bambooChunkBody;
            // Add one to bamboo size
            size += 1;
            // Instantiate new bamboo chunk at the top of the last chunk 
            Vector3 newBambooChunkOffset = new Vector3(bambooPositionX, size - 1 + bambooPositionY, bambooPositionZ);
            GameObject newBambooChunk = Instantiate(bambooChunk, newBambooChunkOffset, Quaternion.identity);
            newBambooChunk.GetComponent<SpriteRenderer>().sprite = bambooChunkTop;
            // Add new bamboo chunk ID to bamboo chunks list
            bambooChunks.Add(newBambooChunk.GetInstanceID().ToString());
            newBambooChunk.name = newBambooChunk.GetInstanceID().ToString();   
        }
    }
}
