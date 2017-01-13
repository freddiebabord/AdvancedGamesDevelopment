using UnityEngine;
using System.Collections;

public class AnimationEventReciever : MonoBehaviour {

    GhostThrow ghostThrow;

	void Start()
	{
		ghostThrow = GetComponentInParent<GhostThrow> ();
	}

	void SpawnGlob()
    {
        ghostThrow.SpawnGlob();
    }

    void ThrowGlob()
    {
        ghostThrow.ThrowGlob();
    }

	void Reset()
	{
		ghostThrow.Reset ();
	}
}
