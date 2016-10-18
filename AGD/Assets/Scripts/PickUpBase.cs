using UnityEngine;
using System.Collections;

public class PickUpBase : MonoBehaviour {

    public enum ItemMap { Higher, Lower, Same, Nullus };
    public ItemMap itemMap = ItemMap.Nullus;
    public bool firstPass = false;

    void OnTriggerEnter(Collider other)
    {
        Radar[] players = FindObjectsOfType<Radar>();

        for(int i = 0; i < players.Length; i++)
        {
            players[i].RemoveRadarObject(this.gameObject);
        }
    }

 
}
