using UnityEngine;
using System.Collections;
public enum PickupType
{
    SpeedBoost,
    WeaponRecharge,
    PowerBoost
}

public class PickupBaseHandler : MonoBehaviour {

   
    public PickupType pickupType;
    
}
