using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MakingUI : MonoBehaviour {

    GameObject the_canvas;
    GameObject slider_object;
    Slider the_slider;

    // Use this for initialization
    void Start () {

        the_canvas = GameObject.FindObjectOfType<CanvasGroup>().gameObject;

        slider_object = new GameObject("SliderObj");
        slider_object.transform.SetParent(the_canvas.transform);

        the_slider = slider_object.AddComponent<Slider>();
    }
}
