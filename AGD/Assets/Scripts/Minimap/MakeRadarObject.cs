using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MakeRadarObject : MonoBehaviour {

    public Image image;
    public Image image_up;

    bool is_higher = false;

	// Use this for initialization
	void Start ()
    {
        if (this.gameObject.transform.position.y > GameObject.FindGameObjectWithTag("Player").transform.position.y)
        {
            Radar.RegisterRadarObject(this.gameObject, image_up);
            is_higher = true;
        }
        else
            Radar.RegisterRadarObject(this.gameObject, image);
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(!is_higher && this.gameObject.transform.position.y > GameObject.FindGameObjectWithTag("Player").transform.position.y)
        {
            Radar.RemoveRadarObject(this.gameObject);
            Radar.RegisterRadarObject(this.gameObject, image_up);
            is_higher = true;
        }
        else if(is_higher && this.gameObject.transform.position.y < GameObject.FindGameObjectWithTag("Player").transform.position.y)
        {
            Radar.RemoveRadarObject(this.gameObject);
            Radar.RegisterRadarObject(this.gameObject, image);
            is_higher = false;
        }
    }

    void OnDestroy()
    {
        Radar.RemoveRadarObject(this.gameObject);
        is_higher = false;
    }
}
