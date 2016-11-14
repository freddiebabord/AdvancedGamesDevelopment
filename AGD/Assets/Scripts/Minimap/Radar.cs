using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

//General struct for the objects that will be shown in the radar.
public class RadarObject
{
    public Image icon { get; set; }
    public GameObject owner { get; set; }
}

public class Radar : NetworkBehaviour {

	GameObject radar;

	//Play with this map_scale to bring the objects either closer together or further away in the radar.
	public float map_scale = 5.0f;

	public static List<RadarObject> radar_objects = new List<RadarObject>();

	void Start()
	{
		radar = GameObject.FindGameObjectWithTag("Radar");
	}
		
    //This will add objects to the radar.
    public static void RegisterRadarObject(GameObject o, Image i)
    {
        Image image = Instantiate(i);
        radar_objects.Add(new RadarObject() { owner = o, icon = image });
    }

    //This will remove objects from the radar when they get destroyed or picked up.
    public void RemoveRadarObject(GameObject o)
    {
        List<RadarObject> new_list = new List<RadarObject>();

        for(int i = 0; i < radar_objects.Count; i++)
        {
            if (radar_objects[i].owner == o)
            {
				Destroy(radar_objects[i].icon.gameObject);
                continue;
            }
            else
                new_list.Add(radar_objects[i]);
        }

        radar_objects.RemoveRange(0, radar_objects.Count);
        radar_objects.AddRange(new_list);
    }

    //Draw the objects in the radar if they are within the bounds of the radar.
    void DrawRadar()
    {
        foreach(RadarObject rad_obj in radar_objects)
        {

            if (rad_obj.owner.gameObject == null)
                continue;
			
			Vector3 radar_position = rad_obj.owner.transform.position - gameObject.transform.position;
			float distance_object = Vector3.Distance (gameObject.transform.position, rad_obj.owner.transform.position) * map_scale;
            float delta_y = Mathf.Atan2(radar_position.x, radar_position.z) * Mathf.Rad2Deg - 270 - gameObject.transform.eulerAngles.y;
			radar_position.x = distance_object * Mathf.Cos(delta_y * Mathf.Deg2Rad) * -1 + (radar.GetComponent<RectTransform>().rect.width/2);
			radar_position.z = distance_object * Mathf.Sin(delta_y * Mathf.Deg2Rad) + (radar.GetComponent<RectTransform>().rect.height/2);

			rad_obj.icon.transform.SetParent(radar.gameObject.transform);
			rad_obj.icon.transform.position = new Vector3(radar_position.x, radar_position.z, 0) + radar.gameObject.transform.position;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(isLocalPlayer)
            DrawRadar();
	
	}
}
