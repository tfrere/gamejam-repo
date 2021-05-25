using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class BackgroundMusic : MonoBehaviour
{

    private SoundHandler soundHandler;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1) { Destroy(this.gameObject); }
        DontDestroyOnLoad(this.gameObject);
    }

    void StartMusic(string name) {
        if(name == "menu") {
            soundHandler.ChangeTheSound(0);
        }
        if(name=="game") {
            soundHandler.ChangeTheSound(Random.Range(0, 3));
        }
    }

    void Start()
    {
        soundHandler = GetComponent<SoundHandler>();
        GameEvents.current.OnStartMusic += StartMusic;
    }


}   