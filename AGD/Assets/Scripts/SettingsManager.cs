using System;
using UnityEngine;
using System.Collections;

public class SettingsManager : MonoBehaviour
{

    public static SettingsManager instance;

    void Awake()
    {
        if(instance)
            Destroy(this.gameObject);
        instance = this;
        DontDestroyOnLoad(this);
    }

    public SplitscreenManager.SplitscreenMode splitscreenMode = SplitscreenManager.SplitscreenMode.Horizontal;
    public float overallVolumeLevel = 100;

    public void SetSplitscreenMode(Int32 value)
    {
        if(value == 0)
            splitscreenMode = SplitscreenManager.SplitscreenMode.Horizontal;
        else
            splitscreenMode = SplitscreenManager.SplitscreenMode.Vertical;
    }

    public void SetMasterVolume(Single volume)
    {
        overallVolumeLevel = volume;
    }
}
