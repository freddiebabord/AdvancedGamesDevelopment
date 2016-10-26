using UnityEngine;
using System.Collections;

public class UnlimitedStamina : PickUpBase {

    public override void ApplyEffect()
    {
        player.GetComponent<PickUps>().stamina.value = player.GetComponent<PickUps>().stamina.maxValue;
    }

}
