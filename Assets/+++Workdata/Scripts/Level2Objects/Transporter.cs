using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transporter : MonoBehaviour
{
    [SerializeField] private GameObject transporterWithoutWood;
    [SerializeField] private GameObject transporterWithWood;
    [SerializeField] private Image transporterImage;
    [SerializeField] private Sprite transporterWithoutWoodSprite;
    [SerializeField] private Sprite transporterWithWoodSprite;

    private void Awake()
    {
        transporterWithoutWood.SetActive(GameStateManager.instance.truckWithoutWood);
        transporterWithWood.SetActive(GameStateManager.instance.truckWithWood);
        transporterImage.sprite = GameStateManager.instance.truckWithoutWood
            ? transporterWithoutWoodSprite
            : transporterWithWoodSprite;
    }
}
