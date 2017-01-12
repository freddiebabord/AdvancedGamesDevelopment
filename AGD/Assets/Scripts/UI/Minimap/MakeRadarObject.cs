using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum PositionMap { Higher, Lower, Same, Nullus };

[System.Serializable]
public class PickupGameObject
{
    public Transform targetTransform;
    public PositionMap positionMap = PositionMap.Nullus;
    public bool firstPass = false;
}


public class MakeRadarObject : MonoBehaviour {

    public Image image;
    public Image image_up;
	public Image image_down;

    List<PickupGameObject> enemies = new List<PickupGameObject>();

    public Image item;
    public Image item_up;
    public Image item_down;

    List<PickupGameObject> collectables = new List<PickupGameObject>();

    Radar radar;

    void Awake()
    {
        enemies = new List<PickupGameObject>();
        collectables = new List<PickupGameObject>();
    }

    void Start()
    {
        radar = gameObject.GetComponent<Radar>();
    }

    public void RegisterEnemy(EnemyBase newEnemy)
    {
        PickupGameObject newPickupGameObject = new PickupGameObject();
        newPickupGameObject.targetTransform = newEnemy.transform;
        enemies.Add(newPickupGameObject);
    }

    public void DeregisterEnemy(EnemyBase enemyToDestroy)
    {
        var enemy = enemies.Find(x => x.targetTransform == enemyToDestroy.transform);
        enemies.Remove(enemy);
        enemies.TrimExcess();
    }

    public void RegisterPickup(PickUpBase pickup)
    {
        if (collectables.Find(x => x.targetTransform == pickup.transform) != null)
            return;
        PickupGameObject newPickupGameObject = new PickupGameObject();
        newPickupGameObject.targetTransform = pickup.transform;
        collectables.Add(newPickupGameObject);
    }

    public void DeregisterPickup(PickUpBase pickup)
    {
        var collectable = collectables.Find(x => x.targetTransform == pickup.transform);
        collectables.Remove(collectable);
        collectables.TrimExcess();
    }
    
    // Update is called once per frame
    void Update ()
	{
	    //if (!isLocalPlayer)
	    //    return;

        // TODO: This should not be done each frame, this is a slow Unity oberation -> again consider using Unity event calls such as Start() and OnDestroy()
		//enemies = FindObjectsOfType<EnemyBase> ().ToList();
        //collectables = FindObjectsOfType<PickUpBase>().ToList();
        
        #region EnemiesOnRadar
        for (int i = 0; i < enemies.Count; i++)
		{

            if (enemies[i].targetTransform.gameObject == null)
            {
                radar.RemoveRadarObject(enemies[i].targetTransform.gameObject);
                continue;
            }

			if (transform.position.y - enemies[i].targetTransform.position.y > 3) 
			{
				if (enemies [i].positionMap !=PositionMap.Lower) 
				{
                    radar.RemoveRadarObject (enemies[i].targetTransform.gameObject);
                    radar.RegisterRadarObject (enemies [i].targetTransform.gameObject, image_down);
					enemies [i].positionMap =PositionMap.Lower;
				}

				if (!enemies [i].firstPass) 
				{
                    radar.RemoveRadarObject(enemies[i].targetTransform.gameObject);
                    radar.RegisterRadarObject (enemies[i].targetTransform.gameObject, image_down);
                    enemies[i].positionMap =PositionMap.Lower;
                    enemies [i].firstPass = true;
				}

			}
			else if (transform.position.y - enemies[i].targetTransform.gameObject.transform.position.y < -3) 
			{
				if (enemies[i].positionMap !=PositionMap.Higher) 
				{
                    radar.RemoveRadarObject (enemies[i].targetTransform.gameObject);
                    radar.RegisterRadarObject (enemies[i].targetTransform.gameObject, image_up);
                    enemies[i].positionMap =PositionMap.Higher;
                }

				if (!enemies [i].firstPass) 
				{
                    radar.RemoveRadarObject(enemies[i].targetTransform.gameObject);
                    radar.RegisterRadarObject (enemies[i].targetTransform.gameObject, image_up);
                    enemies[i].positionMap =PositionMap.Higher;
                    enemies [i].firstPass = true;
				}
			}
			else
			{
				if (enemies[i].positionMap !=PositionMap.Same) 
				{
                    radar.RemoveRadarObject (enemies[i].targetTransform.gameObject);
                    radar.RegisterRadarObject (enemies[i].targetTransform.gameObject, image);
                    enemies[i].positionMap =PositionMap.Same;
                }

				if (!enemies [i].firstPass) 
				{
                    radar.RemoveRadarObject(enemies[i].targetTransform.gameObject);
                    radar.RegisterRadarObject (enemies[i].targetTransform.gameObject, image);
                    enemies[i].positionMap =PositionMap.Same;
                    enemies [i].firstPass = true;
				}
                
			} 
            
        }
        #endregion

        #region PowerUpsOnRadar
        for (int i = 0; i < collectables.Count; i++)
        {
            if (collectables[i].targetTransform.gameObject == null)
            {
                radar.RemoveRadarObject(collectables[i].targetTransform.gameObject);
                continue;

            }

            if (transform.position.y - collectables[i].targetTransform.gameObject.transform.position.y > 3)
            {
                if (collectables[i].positionMap != PositionMap.Lower)
                {
                    radar.RemoveRadarObject(collectables[i].targetTransform.gameObject);
                    radar.RegisterRadarObject(collectables[i].targetTransform.gameObject, item_down);
                    collectables[i].positionMap = PositionMap.Lower;
                }

                if (!collectables[i].firstPass)
                {
                    radar.RegisterRadarObject(collectables[i].targetTransform.gameObject, item_down);
                    collectables[i].positionMap = PositionMap.Lower;
                    collectables[i].firstPass = true;
                }

            }
            else if (transform.position.y - collectables[i].targetTransform.gameObject.transform.position.y < -3)
            {
                if (collectables[i].positionMap != PositionMap.Higher)
                {
                    radar.RemoveRadarObject(collectables[i].targetTransform.gameObject);
                    radar.RegisterRadarObject(collectables[i].targetTransform.gameObject, item_up);
                    collectables[i].positionMap = PositionMap.Higher;
                }

                if (!collectables[i].firstPass)
                {
                    radar.RegisterRadarObject(collectables[i].targetTransform.gameObject, item_up);
                    collectables[i].positionMap = PositionMap.Higher;
                    collectables[i].firstPass = true;
                }
            }
            else
            {
                if (collectables[i].positionMap != PositionMap.Same)
                {
                    radar.RemoveRadarObject(collectables[i].targetTransform.gameObject);
                    radar.RegisterRadarObject(collectables[i].targetTransform.gameObject, item);
                    collectables[i].positionMap = PositionMap.Same;
                }

                if (!collectables[i].firstPass)
                {
                    radar.RemoveRadarObject(collectables[i].targetTransform.gameObject);
                    radar.RegisterRadarObject(collectables[i].targetTransform.gameObject, item);
                    collectables[i].positionMap = PositionMap.Same;
                    collectables[i].firstPass = true;
                }

            }
        }
        #endregion
    }
}
