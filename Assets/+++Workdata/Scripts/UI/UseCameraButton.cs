using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseCameraButton : MonoBehaviour
{
    private Animator loadCameraScreen;

    //do the camera animation for activating the camera
    public void SetAnimationForCamera()
    {
        loadCameraScreen = GameObject.FindGameObjectWithTag("LoadCameraScreen").GetComponent<Animator>();
        loadCameraScreen.SetTrigger("FadeIn");
    }
}
