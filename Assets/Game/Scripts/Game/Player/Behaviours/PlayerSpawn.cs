using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSpawn : MonoBehaviour
{
    private Animator spawnAnimator;

    void Start()
    {
        spawnAnimator = GetComponent<Animator>();
        spawnAnimator.SetTrigger("isTriggered");
    }
}
