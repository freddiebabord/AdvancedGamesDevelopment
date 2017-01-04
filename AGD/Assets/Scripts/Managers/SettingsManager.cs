using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
        QualitySettings.SetQualityLevel(0, true);

    }

    public SplitscreenManager.SplitscreenMode splitscreenMode = SplitscreenManager.SplitscreenMode.Horizontal;
    public float overallVolumeLevel = 100;
    public bool splitscreenDuelControllerMode = true;
    public List<PostProcessingProfile> profiles = new List<PostProcessingProfile>();
    public float lookSensitivity = 0.5f;
    public bool invertYAxis = false;


    public void RegisterPostProfile(PostProcessingProfile profile)
    {
        if(!profiles.Contains(profile))
            profiles.Add(profile);  
    }

    public void SetSplitscreenMode(Int32 value)
    {
        if(value == 0)
            splitscreenMode = SplitscreenManager.SplitscreenMode.Horizontal;
        else
            splitscreenMode = SplitscreenManager.SplitscreenMode.Vertical;
    }

    public void SplitscreenDuelControllerMode(bool enabled)
    {
        splitscreenDuelControllerMode = enabled;
    }

    public void SetMasterVolume(Single volume)
    {
        overallVolumeLevel = volume;
    }

    public void SetLookSensitivity(float sensitivity)
    {
        lookSensitivity = sensitivity;
    }

    public void SetInvertYAxis(bool invert)
    {
        invertYAxis = invert;
    }

    public void SetAAMode(int value)
    {
        if(value == 0)
        {
            foreach(var profile in profiles)
                profile.antialiasing.enabled = false;
        }
        else if(value == 1)
        {
            foreach (var profile in profiles)
            {
                profile.antialiasing.enabled = true;
                var aaSettings = profile.antialiasing.settings;
                aaSettings.method = AntialiasingModel.Method.Fxaa;
                profile.antialiasing.settings = aaSettings;
            }
        }
        else if(value == 2)
        {
            foreach (var profile in profiles)
            {
                profile.antialiasing.enabled = true;
                var aaSettings = profile.antialiasing.settings;
                aaSettings.method = AntialiasingModel.Method.Taa;
                profile.antialiasing.settings = aaSettings;
            }
        }
    }

    public void SetBloomIntensity(float intensity)
    {
        foreach (var profile in profiles)
        {
            var bloomSettigns = profile.bloom.settings;
            bloomSettigns.bloom.intensity = intensity;
            profile.bloom.settings = bloomSettigns;
        }
    }

    public void SetReflectionQuality(int value)
    {
        if (value == 0)
        {
            QualitySettings.realtimeReflectionProbes = false;
            foreach (var profile in profiles)
            {
                profile.screenSpaceReflection.enabled = false;
            }

        }
        else if(value == 1)
        {
            QualitySettings.realtimeReflectionProbes = true;
            foreach (var profile in profiles)
            {
                profile.screenSpaceReflection.enabled = true;
                var screnSpaceReflectionQuality = profile.screenSpaceReflection.settings;
                screnSpaceReflectionQuality.reflection.reflectionQuality = ScreenSpaceReflectionModel.SSRResolution.Low;
                profile.screenSpaceReflection.settings = screnSpaceReflectionQuality;
            }
        }
        else
        {
            QualitySettings.realtimeReflectionProbes = true;
            foreach (var profile in profiles)
            {
                profile.screenSpaceReflection.enabled = true;
                var screnSpaceReflectionQuality = profile.screenSpaceReflection.settings;
                screnSpaceReflectionQuality.reflection.reflectionQuality = ScreenSpaceReflectionModel.SSRResolution.High;
                profile.screenSpaceReflection.settings = screnSpaceReflectionQuality;
            }
        }
    }

    public void SetVsync(int value)
    {
        QualitySettings.vSyncCount = value;
    }

	public void Setfullscreen(bool fullscreen_)
	{
		Screen.fullScreen = fullscreen_;
	}

	public void SetResolution(int value)
	{
		Screen.SetResolution (NetManager.Instance.resolutions [value].width, NetManager.Instance.resolutions [value].height, Screen.fullScreen);
	}
}
