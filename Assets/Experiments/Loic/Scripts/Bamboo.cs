using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bamboo : MonoBehaviour
{
    public Transform bambooChunk;
    public int size = 1;

    void Start()
    {
        for(int i = size; i <= size; i++)
        {
            Instantiate(bambooChunk, new Vector3(i * 1.0F, 0, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
