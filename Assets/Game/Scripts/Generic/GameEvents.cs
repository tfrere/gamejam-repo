using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        if (current == null) {
            current = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public event Action<string> OnStartMusic;
    public void StartMusicTrigger(string name) {
        print("Event : Change music for " + name);
        if(OnStartMusic != null) {
            OnStartMusic(name);
        }
    }

    public event Action<string> OnChangeInputScheme;
    public void ChangeInputSchemeTrigger(string name) {
        print("Event : Change input scheme for " + name);
        if(OnChangeInputScheme != null) {
            OnChangeInputScheme(name);
        }
    }
    
}