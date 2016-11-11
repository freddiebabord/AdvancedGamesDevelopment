using System;
using UnityEngine;
using System.Collections;
using UnityEngine.PostProcessing;

public enum AntiAlisatingMode
{
    Off,
    FXAA,
    TXAA
}

public enum ReflectionQuality
{
    Off,
    Low,
    High
}

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
    public bool splitscreenDuelControllerMode = true;
    public float bloomIntensity = 1;
    public AntiAlisatingMode aaMode = AntiAlisatingMode.TXAA;
    public ReflectionQuality reflectionQuality;
    public PostProcessingProfile profile;

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

    public void SetAAMode(int value)
    {
        if(value == 0)
        {
            aaMode = AntiAlisatingMode.Off;
            if(profile)
                profile.antialiasing.enabled = false;
        }
        if(value == 1)
        {
            aaMode = AntiAlisatingMode.FXAA;
            if(profile)
            {
                profile.antialiasing.enabled = true;
                profile.antialiasing.settings.method = AntialiasingModel.Method.Fxaa;
            }
        }
        if(value == 2)
        {
            aaMode = AntiAlisatingMode.TXAA;
            if(profile)
            {
                profile.antialiasing.enabled = true;
            }
        }
    }
}
