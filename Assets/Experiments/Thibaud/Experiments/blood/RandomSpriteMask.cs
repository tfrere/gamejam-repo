using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteMask : MonoBehaviour
{
    public Sprite[] sprites;
    void Awake()
    {
        GetComponent<SpriteMask>().sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
