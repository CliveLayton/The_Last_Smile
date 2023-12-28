using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementChecker : MonoBehaviour
{
    private PlayerController player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void StopPlayerMovement()
    {
        player.inSequence = true;
    }

    public void StartPlayerMovement()
    {
        player.inSequence = false;
    }
}
