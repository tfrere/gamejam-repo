using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChoosingMenu : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("PlayerConfiguration-0").GetComponent<PlayerInstanciationController>().handleInstanciate(0, new Vector3(0,0,0));
    }

}

