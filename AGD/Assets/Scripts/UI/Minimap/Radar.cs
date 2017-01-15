using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
//General struct for the objects that will be shown in the radar.
public class RadarObject
{
    public Image icon { get; set; }
    public GameObject owner { get; set; }
}

public class Radar : MonoBehaviour {

	public GameObject radar;

	//Play with this map_scale to bring the objects either closer together or further away in the radar.
	public float map_scale = 0.1f;

    [SerializeField]
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
        var newRad = new RadarObject() {owner = o, icon = image};
        radar_objects.Add(newRad);
        var rt = newRad.icon.GetComponent<RectTransform>();
        rt.SetParent(radar.transform);
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
    }

    //This will remove objects from the radar when they get destroyed or picked up.
    public void RemoveRadarObject(GameObject o)
    {
        var idx = radar_objects.Find(x => x.owner == o);
        if (idx != null)
        {
            Destroy(idx.icon.gameObject);
            radar_objects.Remove(idx);
            radar_objects.TrimExcess();
        }
        //    List<RadarObject> new_list = new List<RadarObject>();

        //    for(int i = 0; i < radar_objects.Count; i++)
        //    {
        //        if (radar_objects[i].owner == o)
        //        {
        //Destroy(radar_objects[i].icon.gameObject);
        //            continue;
        //        }
        //        else
        //            new_list.Add(radar_objects[i]);
        //    }

        //    radar_objects.RemoveRange(0, radar_objects.Count);
        //    radar_objects.AddRange(new_list);
    }

    


    //Draw the objects in the radar if they are within the bounds of the radar.
    void DrawRadar()
    {
        for (int i = 0; i < radar_objects.Count; ++i)
        {

			if (radar_objects [i].owner.gameObject == null) {
				if(radar_objects [i].icon)
					Destroy (radar_objects [i].icon.gameObject);
				radar_objects.RemoveAt (i);
				continue;
			}


            // Pulled and adapted from: http://wiki.unity3d.com/index.php?title=Radar - Freddie Babord

            Vector3 centerPos = transform.position;
            Vector3 extPos = radar_objects[i].owner.transform.position;

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

            RectTransform rt = radar_objects[i].icon.GetComponent<RectTransform>();
			//- halfRadarDims.x/2
            rt.localPosition = new Vector3(bX , bY, -1);// + radar.transform.position;
            rt.localScale = new Vector3(1, 1, 1);
        }
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        DrawRadar();
	}

}
