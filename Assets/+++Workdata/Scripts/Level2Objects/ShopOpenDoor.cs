using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOpenDoor : MonoBehaviour
{
    private CircleCollider2D shopOpenCollider;
    
    private void Awake()
    {
        shopOpenCollider = gameObject.GetComponentInChildren<CircleCollider2D>();
        shopOpenCollider.enabled = GameStateManager.instance.shopOpenDoorActive;
    }
}
