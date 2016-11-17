using UnityEngine;
using System.Collections;
public enum PickupType
{
    SpeedBoost,
    WeaponRecharge,
    PowerBoost,
    NullifyFear
}

public class PickupBaseHandler : MonoBehaviour {

   
    public PickupType pickupType;
    
}
