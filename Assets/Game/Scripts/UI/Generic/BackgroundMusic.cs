using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

// Object.DontDestroyOnLoad example.
//
// This script example manages the playing audio. The GameObject with the
// "music" tag is the BackgroundMusic GameObject. The AudioSource has the
// audio attached to the AudioClip.

public class BackgroundMusic : MonoBehaviour
{

    private SoundHandler soundHandler;

    private string oldGameState = "menu";

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1) { Destroy(this.gameObject); }
        DontDestroyOnLoad(this.gameObject);
    }

    void LaunchMenuMusic() {
        print("background music recieved event menulaunch");
        Invoke("LaunchMusicActivation", .3f);
    }

    void LaunchGameMusic() {
        print("game music recieved event gamelaunch");
        Invoke("LaunchMusicActivation", .3f);
    }

    void Start()
    {
        soundHandler = GetComponent<SoundHandler>();
        LaunchMenuMusic();
    }


    void Update() {
        if(GameInfo.GameState != oldGameState && GameInfo.GameState == "menu") {
            LaunchMenuMusic();
            print("launch music menu");
        }
        if(GameInfo.GameState != oldGameState && GameInfo.GameState == "game") {
            LaunchGameMusic();
            print("launch game menu");
        }
        oldGameState = GameInfo.GameState;
    }

    void LaunchMusic() {
        Invoke("LaunchMusicActivation", .3f);
    }
    void LaunchMusicActivation()
    {
        if(GameInfo.GameState == "menu") {
            soundHandler.ChangeTheSound(0);
        }
        if(GameInfo.GameState == "game") {
            soundHandler.ChangeTheSound(Random.Range(0, 3));
        }
    }


}   