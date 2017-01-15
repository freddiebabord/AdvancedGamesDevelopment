using UnityEngine;
using System.Collections;

public class AnimationEventReciever : MonoBehaviour {

    GhostThrow ghostThrow;
    GhostTeleport ghostTeleport;

	void Start()
	{
		ghostThrow = GetComponentInParent<GhostThrow> ();
        ghostTeleport = GetComponentInParent<GhostTeleport>();
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
		ghostThrow.Reset();
	}

    void Teleport()
    {
        ghostTeleport.StartTeleport();
    }
}
