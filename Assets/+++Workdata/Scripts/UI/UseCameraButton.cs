using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseCameraButton : MonoBehaviour
{
    private Animator loadCameraScreen;

    public void SetAnimationForCamera()
    {
        loadCameraScreen = GameObject.FindGameObjectWithTag("LoadCameraScreen").GetComponent<Animator>();
        loadCameraScreen.SetTrigger("FadeIn");
    }
}