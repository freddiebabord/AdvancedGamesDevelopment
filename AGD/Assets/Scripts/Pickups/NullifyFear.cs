using UnityEngine;
using System.Collections;

public class NullifyFear : PickUpBase {

    public override void ApplyEffect()
    {
        //player.GetComponent<PickUps>().
        //Debug.Log(player.GetComponent<PickUps>().fear_increase.ToString());

        player.GetComponent<PickUps>().fear.value += player.GetComponent<PickUps>().fear_increase;
    }
}
