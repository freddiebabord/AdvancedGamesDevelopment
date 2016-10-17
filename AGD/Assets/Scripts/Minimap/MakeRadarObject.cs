using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


public class MakeRadarObject : NetworkBehaviour {

    public Image image;
    public Image image_up;
	public Image image_down;

    EnemyBase[] enemies;


		
	// Update is called once per frame
	void Update ()
	{
	    if (!isLocalPlayer)
	        return;

		enemies = FindObjectsOfType<EnemyBase> ();

        for (int i = 0; i < enemies.Length; i++)
		{

			if (gameObject.transform.position.y - enemies [i].gameObject.transform.position.y > 1) 
			{
				if (!enemies [i].isLower) 
				{
					Radar.RemoveRadarObject (enemies [i].gameObject);
					Radar.RegisterRadarObject (enemies [i].gameObject, image_down);
					enemies [i].isHigher = false;
					enemies [i].isLower = true;
					enemies [i].isSame = false;
				}

				if (!enemies [i].firstPass) 
				{
					Radar.RegisterRadarObject (enemies [i].gameObject, image_down);
					enemies [i].isHigher = false;
					enemies [i].isLower = true;
					enemies [i].isSame = false;
					enemies [i].firstPass = true;
				}

			}
			else if (gameObject.transform.position.y - enemies [i].gameObject.transform.position.y < 0) 
			{
				if (!enemies [i].isHigher) 
				{
					Radar.RemoveRadarObject (enemies [i].gameObject);
					Radar.RegisterRadarObject (enemies [i].gameObject, image_up);
					enemies [i].isHigher = true;
					enemies [i].isLower = false;
					enemies [i].isSame = false;
				}

				if (!enemies [i].firstPass) 
				{
					Radar.RegisterRadarObject (enemies [i].gameObject, image_up);
					enemies [i].isHigher = true;
					enemies [i].isLower = false;
					enemies [i].isSame = false;
					enemies [i].firstPass = true;
				}
			}
			else
			{
				if (!enemies[i].isSame) 
				{
					Radar.RemoveRadarObject (enemies [i].gameObject);
					Radar.RegisterRadarObject (enemies [i].gameObject, image);
					enemies [i].isHigher = false;
					enemies [i].isSame = true;
					enemies [i].isLower = false;
				}

				if (!enemies [i].firstPass) 
				{
					Radar.RegisterRadarObject (enemies [i].gameObject, image);
					enemies [i].isHigher = false;
					enemies [i].isLower = false;
					enemies [i].isSame = true;
					enemies [i].firstPass = true;
				}
                
			} 
            
        }
    }
}
