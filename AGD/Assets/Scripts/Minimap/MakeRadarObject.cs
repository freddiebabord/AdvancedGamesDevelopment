using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class MakeRadarObject : MonoBehaviour {

    public Image image;
    public Image image_up;

    EnemyBase[] enemies;


	// Use this for initialization
	void Start ()
    {
        enemies = FindObjectsOfType<EnemyBase>();

        Debug.Log(enemies.Length);

        for (int i = 0; i < enemies.Length; i++)
        {
            if (this.gameObject.transform.position.y < enemies[i].gameObject.transform.position.y)
            {
                Radar.RegisterRadarObject(enemies[i].gameObject, image_up);
            }
            else
                Radar.RegisterRadarObject(enemies[i].gameObject, image);
        }
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        enemies = FindObjectsOfType<EnemyBase>();

        for (int i = 0; i < enemies.Length; i++)
        {
            if (gameObject.transform.position.y > enemies[i].gameObject.transform.position.y)
            {
                Radar.RemoveRadarObject(enemies[i].gameObject);
                Radar.RegisterRadarObject(enemies[i].gameObject, image);
            }
            else if (gameObject.transform.position.y < enemies[i].gameObject.transform.position.y)
            {
                Radar.RemoveRadarObject(enemies[i].gameObject);
                Radar.RegisterRadarObject(enemies[i].gameObject, image_up);
            }
            else
                continue;
        }
    }
}
