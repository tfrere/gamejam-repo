using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UISoundManager : MonoBehaviour
{

    private SoundHandler soundHandler;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1) { Destroy(this.gameObject); }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        soundHandler = GetComponent<SoundHandler>();
        GameEvents.current.OnUISound += StartSound;
    }

    void StartSound(string name)

    {
        print("sound");
        if (name == "navigate")
        {
            soundHandler.ChangeTheSound(0);
        }
        if (name == "validate")
        {
            soundHandler.ChangeTheSound(1);
        }
        if (name == "countdown")
        {
            soundHandler.ChangeTheSound(2);
        }
    }

}