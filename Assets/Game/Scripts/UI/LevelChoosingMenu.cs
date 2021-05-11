using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChoosingMenu : MonoBehaviour
{
    private PlayerInstanciationController controller;
    public GameObject playerOne;


    void Start()
    {
        controller = GameObject.Find("PlayerConfiguration-0").GetComponent<PlayerInstanciationController>();
        controller.handleInstanciate(0);
    }

}

