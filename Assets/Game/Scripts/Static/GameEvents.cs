using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
        }
    }

    public event Action<int> MenuLaunchTrigger;
    public event Action<int> GameLaunchTrigger;

    // make a sound on space enter while UI is loaded
    public void OnMenuLaunchTrigger() {
        print(0);
        // MenuLaunch?.Invoke(id);
    }

    public void OnGameLaunchTrigger() {
        print(0);
        // GameLaunch?.Invoke(id);
    }
}