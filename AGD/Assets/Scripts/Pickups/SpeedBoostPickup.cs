using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class SpeedBoostPickup : Pickup
{

    public int multiplyer;
    public float durationTimer;

    protected override void PickupLogic(GameObject player)
    {
        player.GetComponent<NetworkedThirdPersonUserControl>().multiplyer = multiplyer;
        StartCoroutine( ResetMultiplier(player.GetComponent<NetworkedThirdPersonUserControl>()));
    }

    IEnumerator ResetMultiplier(NetworkedThirdPersonUserControl player)
    {
        yield return new WaitForSeconds(durationTimer);
        player.multiplyer = 1;
    }
}
