using UnityEngine;
using System.Collections;

public class PickUpBase : MonoBehaviour {

    public enum ItemMap { Higher, Lower, Same, Nullus };
    public ItemMap itemMap = ItemMap.Nullus;
    public bool firstPass = false;

 
}
