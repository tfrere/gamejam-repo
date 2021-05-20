using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public bool isAvailableForSpawnPlayer = true;


    void OnTriggerEnter2D(Collider2D col) {
        if(col.tag.Contains("Player")){
            isAvailableForSpawnPlayer = false;
        }
    }

    void OnTriggerStay2D(Collider2D col) {
        if(col.tag.Contains("Player")){
            isAvailableForSpawnPlayer = false;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        isAvailableForSpawnPlayer = true;
    }

}
