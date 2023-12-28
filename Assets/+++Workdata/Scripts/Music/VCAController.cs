using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using UnityEngine.UI;

public class VCAController : MonoBehaviour
{
    /// <summary>
    /// link to the VCA in FMOD
    /// </summary>
    private VCA vCAController;

    public string vCAName;

    [SerializeField] private float vCAVolume;

    private Slider slider;

    /// <summary>
    /// gets components and set the volume of the slider and the VCA to the Volume variable
    /// </summary>
    private void Start()
    {
        vCAController = FMODUnity.RuntimeManager.GetVCA("vca:/" + vCAName);
        slider = GetComponent<Slider>();
        vCAController.getVolume(out vCAVolume);
        slider.value = vCAVolume;
    }

    /// <summary>
    /// sets the volume of the VCA and gets Volume of the VCA
    /// </summary>
    /// <param name="volume">float to change the volume</param>
    public void SetVolume(float volume)
    {
        vCAController.setVolume(volume);
        vCAController.getVolume(out vCAVolume);
    }
}
