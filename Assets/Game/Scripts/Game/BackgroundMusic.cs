using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object.DontDestroyOnLoad example.
//
// This script example manages the playing audio. The GameObject with the
// "music" tag is the BackgroundMusic GameObject. The AudioSource has the
// audio attached to the AudioClip.

public class BackgroundMusic : MonoBehaviour
{

    private SoundHandler soundHandler;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1) { Destroy(this.gameObject); }
        DontDestroyOnLoad(this.gameObject);
    }
    void OnMenuLaunch() {
        print("background music recieved event menulaunch");
        soundHandler.ChangeTheSound(0);
    }
    void Start()
    {
        soundHandler = GetComponent<SoundHandler>();
        // GameEvents.instance.MenuLaunchTrigger += OnMenuLaunch;
    }

    // public void OnGameLaunch(int id) {
    //     print("background music recieved event gamelaunch");
    //     soundHandler.ChangeTheSound(Random.Range(1, 3));
    // }

}   