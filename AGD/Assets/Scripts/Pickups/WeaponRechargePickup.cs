using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class WeaponRechargePickup : Pickup
{
    
    public float durationTimer;

    protected override void PickupLogic(GameObject player)
    {
        StartCoroutine( ResetMultiplier(player.GetComponent<NetworkedThirdPersonUserControl>()));
    }

    IEnumerator ResetMultiplier(NetworkedThirdPersonUserControl player)
    {
        yield return new WaitForSeconds(durationTimer);
        player.multiplyer = 1;
    }
}
