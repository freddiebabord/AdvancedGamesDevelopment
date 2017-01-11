using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplitscreenManager : MonoBehaviour {

    public List<Camera> cameras = new List<Camera>();
    public enum SplitscreenMode
    {
        Horizontal,
        Vertical
    }

    public SplitscreenMode mode = SplitscreenMode.Horizontal;

    public void RegisterCamera(Camera cam)
    {
        cameras.Add(cam);
    }

    void Start()
    {
        if (!SettingsManager.instance)
            return;
        mode = SettingsManager.instance.splitscreenMode;
        //Invoke("Setup", 0.1f);
    }

    public void Update()
    {
        float fraction = 1f / cameras.Count;
        switch (mode)
        {
            case SplitscreenMode.Vertical:
                for (int i = 0; i < cameras.Count; i++)
                {
                    cameras[i].rect = new Rect(0, fraction * i, 1, fraction);
                    cameras[i].transform.GetChild(0).GetComponent<Camera>().rect = new Rect(0, fraction * i, 1, fraction);
                }
                break;
            case SplitscreenMode.Horizontal:
                for (int i = 0; i < cameras.Count; i++)
                {
                    cameras[i].rect = new Rect(fraction * i, 0, fraction, 1);
                    cameras[i].transform.GetChild(0).GetComponent<Camera>().rect = new Rect(fraction * i, 0, fraction, 1);
                }
                break;
        }
    }
}
