using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script to set the player variable inSequence
/// player can no longer moves if inSequence
/// </summary>
public class MovementChecker : MonoBehaviour
{
    private PlayerController player;
    
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().gameObject.GetComponent<PlayerController>();
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
