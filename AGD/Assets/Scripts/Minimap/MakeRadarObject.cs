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

    public Image item;
    public Image item_up;
    public Image item_down;

    PickUpBase[] collectables;

    // Update is called once per frame
    void Update ()
	{
	    if (!isLocalPlayer)
	        return;

		enemies = FindObjectsOfType<EnemyBase> ();
        collectables = FindObjectsOfType<PickUpBase>();

        for (int i = 0; i < enemies.Length; i++)
		{

            if (enemies[i].gameObject == null)
            {
                Radar.RemoveRadarObject(enemies[i].gameObject);
                continue;
            }

			if (gameObject.transform.position.y - enemies [i].gameObject.transform.position.y > 3.6) 
			{
				if (enemies [i].enemyMap != EnemyBase.EnemyMap.Lower) 
				{
					Radar.RemoveRadarObject (enemies [i].gameObject);
					Radar.RegisterRadarObject (enemies [i].gameObject, image_down);
					enemies [i].enemyMap = EnemyBase.EnemyMap.Lower;
				}

				if (!enemies [i].firstPass) 
				{
					Radar.RegisterRadarObject (enemies [i].gameObject, image_down);
                    enemies[i].enemyMap = EnemyBase.EnemyMap.Lower;
                    enemies [i].firstPass = true;
				}

			}
			else if (gameObject.transform.position.y - enemies [i].gameObject.transform.position.y < -3.6) 
			{
				if (enemies[i].enemyMap != EnemyBase.EnemyMap.Higher) 
				{
					Radar.RemoveRadarObject (enemies [i].gameObject);
					Radar.RegisterRadarObject (enemies [i].gameObject, image_up);
                    enemies[i].enemyMap = EnemyBase.EnemyMap.Higher;
                }

				if (!enemies [i].firstPass) 
				{
					Radar.RegisterRadarObject (enemies [i].gameObject, image_up);
                    enemies[i].enemyMap = EnemyBase.EnemyMap.Higher;
                    enemies [i].firstPass = true;
				}
			}
			else
			{
				if (enemies[i].enemyMap != EnemyBase.EnemyMap.Same) 
				{
					Radar.RemoveRadarObject (enemies [i].gameObject);
					Radar.RegisterRadarObject (enemies [i].gameObject, image);
                    enemies[i].enemyMap = EnemyBase.EnemyMap.Same;
                }

				if (!enemies [i].firstPass) 
				{
					Radar.RegisterRadarObject (enemies [i].gameObject, image);
                    enemies[i].enemyMap = EnemyBase.EnemyMap.Same;
                    enemies [i].firstPass = true;
				}
                
			} 
            
        }

        for (int i = 0; i < collectables.Length; i++)
        {
            if (collectables[i].gameObject == null)
            {
                Radar.RemoveRadarObject(collectables[i].gameObject);
                continue;

            }

            if (gameObject.transform.position.y - collectables[i].gameObject.transform.position.y > 3.6)
            {
                if (collectables[i].itemMap != PickUpBase.ItemMap.Lower)
                {
                    Radar.RemoveRadarObject(collectables[i].gameObject);
                    Radar.RegisterRadarObject(collectables[i].gameObject, item_down);
                    collectables[i].itemMap = PickUpBase.ItemMap.Lower;
                }

                if (!collectables[i].firstPass)
                {
                    Radar.RegisterRadarObject(collectables[i].gameObject, item_down);
                    collectables[i].itemMap = PickUpBase.ItemMap.Lower;
                    collectables[i].firstPass = true;
                }

            }
            else if (gameObject.transform.position.y - collectables[i].gameObject.transform.position.y < -3.6)
            {
                if (collectables[i].itemMap != PickUpBase.ItemMap.Higher)
                {
                    Radar.RemoveRadarObject(collectables[i].gameObject);
                    Radar.RegisterRadarObject(collectables[i].gameObject, item_up);
                    collectables[i].itemMap = PickUpBase.ItemMap.Higher;
                }

                if (!collectables[i].firstPass)
                {
                    Radar.RegisterRadarObject(collectables[i].gameObject, item_up);
                    collectables[i].itemMap = PickUpBase.ItemMap.Higher;
                    collectables[i].firstPass = true;
                }
            }
            else
            {
                if (collectables[i].itemMap != PickUpBase.ItemMap.Same)
                {
                    Radar.RemoveRadarObject(collectables[i].gameObject);
                    Radar.RegisterRadarObject(collectables[i].gameObject, item);
                    collectables[i].itemMap = PickUpBase.ItemMap.Same;
                }

                if (!collectables[i].firstPass)
                {
                    Radar.RegisterRadarObject(collectables[i].gameObject, item);
                    collectables[i].itemMap = PickUpBase.ItemMap.Same;
                    collectables[i].firstPass = true;
                }

            }
        }
    }
}
