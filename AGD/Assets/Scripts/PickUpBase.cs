using UnityEngine;
using System.Collections;

public class PickUpBase : MonoBehaviour {

    public enum ItemMap { Higher, Lower, Same, Nullus };
    public ItemMap itemMap = ItemMap.Nullus;
    public bool firstPass = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<PickUps>().power)
        {
            other.gameObject.GetComponent<PickUps>().Triggered(gameObject.GetComponent<Collider>());

            Radar[] players = FindObjectsOfType<Radar>();

            for (int j = 0; j < players.Length; j++)
            {
                players[j].RemoveRadarObject(gameObject);
            }
            Destroy(gameObject);

        }
    }

 
}
