using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementChecker : MonoBehaviour
{
    private PlayerController player;
    private InGameUI inGameUI;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        inGameUI = GameObject.FindGameObjectWithTag("InGameHUD").GetComponent<InGameUI>();
    }

    public void StopPlayerMovement()
    {
        player.inSequence = true;
    }

    public void StartPlayerMovement()
    {
        player.inSequence = false;
    }

    public void SetTranporterPictureActive(bool state)
    {
        inGameUI.EnableTransporterPicture(state);
    }
}
