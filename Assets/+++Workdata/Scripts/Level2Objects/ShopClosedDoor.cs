using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopClosedDoor : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(GameStateManager.instance.shopClosedDoorActive);
    }
}
