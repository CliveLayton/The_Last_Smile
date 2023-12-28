using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class SettingsMenu : MonoBehaviour
{
    /// <summary>
    /// List of ints for the width screensize
    /// </summary>
    private List<int> widths = new List<int>() { 1280, 1920, 2560 };

    /// <summary>
    /// List of ints for the height screensize
    /// </summary>
    private List<int> heights = new List<int>() { 720, 1200, 1600 };

    /// <summary>
    /// Key to save selected width in playerprefs
    /// </summary>
    private const string resolutionWidth_KEY = "ResolutionWidth";
    
    /// <summary>
    /// Key to save selected height in playerprefs
    /// </summary>
    private const string resolutionHeight_KEY = "ResolutionHeight";

    /// <summary>
    /// Key to save selected index of the lists in playerprefs
    /// </summary>
    private const string resolutionIndex_KEY = "ResolutionIndex";

    /// <summary>
    /// Key to save bool for fullscreen in playerprefs
    /// </summary>
    private const string fullScreen_KEY = "FullScreen";
    
    public Toggle fullScreenToggle;

    public TMP_Dropdown resolutionDropdown;

    private void Start()
    {
        LoadSettings();
    }

    /// <summary>
    /// sets resolution and fullscreen to saved variables in playerprefs
    /// </summary>
    private void LoadSettings()
    {
        resolutionDropdown.value = PlayerPrefs.GetInt(resolutionIndex_KEY, 2);
        fullScreenToggle.isOn = PlayerPrefs.GetInt(fullScreen_KEY, Screen.fullScreen ? 1 : 0) > 0;
        Screen.SetResolution(PlayerPrefs.GetInt(resolutionWidth_KEY, 1920), 
            PlayerPrefs.GetInt(resolutionHeight_KEY, 1200),
            fullScreenToggle.isOn);
    }

    /// <summary>
    /// sets the screen resolution to the int in the list
    /// </summary>
    /// <param name="resolutionIndex">index for lists</param>
    public void SetResolutions(int resolutionIndex)
    {
        int width = widths[resolutionIndex];
        int height = heights[resolutionIndex];
        Screen.SetResolution(width, height, Screen.fullScreen);
        PlayerPrefs.SetInt(resolutionWidth_KEY, width);
        PlayerPrefs.SetInt(resolutionHeight_KEY, height);
        PlayerPrefs.SetInt(resolutionIndex_KEY, resolutionIndex);
    }

    /// <summary>
    /// sets Fullscreen to given bool
    /// </summary>
    /// <param name="isFullscreen">bool for in fullscreen</param>
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt(fullScreen_KEY, isFullscreen ? 1: 0);
    }
}
