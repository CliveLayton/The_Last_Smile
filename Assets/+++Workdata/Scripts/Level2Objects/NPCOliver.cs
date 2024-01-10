using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCOliver : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(GameStateManager.instance.oliverActive);
    }
}
