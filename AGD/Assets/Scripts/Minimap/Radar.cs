using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;

//General struct for the objects that will be shown in the radar.
public class RadarObject
{
    public Image icon { get; set; }
    public GameObject owner { get; set; }
}

public class Radar : NetworkBehaviour {

	public GameObject radar;

	//Play with this map_scale to bring the objects either closer together or further away in the radar.
	public float map_scale = 0.1f;

	public List<RadarObject> radar_objects = new List<RadarObject>();
    private Vector2 radarDims, halfRadarDims;

    void Start()
	{
        //radar = GameObject.FindGameObjectWithTag("Radar");
        radarDims = radar.GetComponent<RectTransform>().rect.size;
        halfRadarDims = radarDims / 2;
    }
		
    //This will add objects to the radar.
    public void RegisterRadarObject(GameObject o, Image i)
    {
        Image image = Instantiate(i);
        radar_objects.Add(new RadarObject() { owner = o, icon = image });
        radar_objects.Last().icon.transform.SetParent(radar.transform);
        radar_objects.Last().icon.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        radar_objects.Last().icon.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
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
        
        foreach (RadarObject rad_obj in radar_objects)
        {

            if (rad_obj.owner.gameObject == null)
                continue;


<<<<<<< HEAD
            // Pulled and adapted from: http://wiki.unity3d.com/index.php?title=Radar - Freddie Babord

            Vector3 centerPos = transform.position;
            Vector3 extPos = rad_obj.owner.transform.position;

            // first we need to get the distance of the enemy from the player
            float dist = Vector3.Distance(centerPos, extPos);

            float dx = centerPos.x - extPos.x; // how far to the side of the player is the enemy?
            float dz = centerPos.z - extPos.z; // how far in front or behind the player is the enemy?

            // what's the angle to turn to face the enemy - compensating for the player's turning?
            float deltay = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg - 270 - transform.eulerAngles.y;

            // just basic trigonometry to find the point x,y (enemy's location) given the angle deltay
            float bX = dist * Mathf.Cos(deltay * Mathf.Deg2Rad);
            float bY = dist * Mathf.Sin(deltay * Mathf.Deg2Rad);

            bX = bX * map_scale; // scales down the x-coordinate by half so that the plot stays within our radar
            bY = bY * -map_scale; // scales down the y-coordinate by half so that the plot stays within our radar

            rad_obj.icon.GetComponent<RectTransform>().localPosition = new Vector3(bX + halfRadarDims.x, bY + halfRadarDims.y, -1);// + radar.transform.position;
            rad_obj.icon.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            continue;



            // Kept James original code below for refrance


   //         Vector3 radar_position = rad_obj.owner.transform.position - gameObject.transform.position;
			//float distance_object = Vector3.Distance (gameObject.transform.position, rad_obj.owner.transform.position) * map_scale;
   //         float delta_y = Mathf.Atan2(radar_position.x, radar_position.z) * Mathf.Rad2Deg - 270 - gameObject.transform.eulerAngles.y;
   //         radar_position.x = distance_object * Mathf.Cos(delta_y * Mathf.Deg2Rad) * -1;
   //         radar_position.z = distance_object * Mathf.Sin(delta_y * Mathf.Deg2Rad);

   //         //Transform trans = GameObject.FindGameObjectWithTag("Parent").transform;

			//rad_obj.icon.transform.SetParent(trans);
   //         rad_obj.icon.transform.position = new Vector3(radar_position.x / radar.GetComponent<RectTransform>().rect.width, radar_position.z / radar.GetComponent<RectTransform>().rect.height, 0) + trans.position;
			
   //         rad_obj.icon.transform.position = new Vector3(radar_position.x, radar_position.z/radar.GetComponent<RectTransform>().rect.height, 0);// + radar.transform.position;
   //         rad_obj.icon.transform.rotation = new Quaternion(0, 0, 0, 0);
=======
			rad_obj.icon.transform.SetParent(trans);
          //  rad_obj.icon.transform.position = new Vector3(radar_position.x / radar.GetComponent<RectTransform>().rect.width, radar_position.z / radar.GetComponent<RectTransform>().rect.height, 0) + trans.position;
         //   rad_obj.icon.transform.rotation = new Quaternion(0, 0, 0, 0);
>>>>>>> parent of b55921d... Radar math updated
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(isLocalPlayer)
            DrawRadar();
	
	}
}
