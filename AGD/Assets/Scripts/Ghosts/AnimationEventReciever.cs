using UnityEngine;
using System.Collections;

public class AnimationEventReciever : MonoBehaviour {

    public GhostThrow ghostThrow;

	void SpawnGlob()
    {
        ghostThrow.SpawnGlob();
    }

    void ThrowGlob()
    {
        ghostThrow.ThrowGlob();
    }
}
