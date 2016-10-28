using UnityEngine;
using System.Collections;

public class PickupBaseHandler : MonoBehaviour {

    public enum PickupType
    {
        SpeedBoost,
        WeaponRecharge,
        PowerBoost
    }

    public PickupType pickupType;
    
}
