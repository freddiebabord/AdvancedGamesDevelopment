using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AudioManager : NetworkBehaviour {

	public static AudioManager inst;
	public List<AudioClip> loadedClips = new List<AudioClip>();
	
	void Awake()
	{
		inst = this;
		loadedClips = ((AudioClip[])Resources.LoadAll("", typeof(AudioClip))).ToList();
	}

	void OnDisable()
	{
		for(int i = 0; i < loadedClips.Count; ++i)
			Resources.UnloadAsset(loadedClips[i]);
	}

	public void PlaySound(AudioClip clip, GameObject source, bool loop)
	{
		Cmd_PlaySound(clip, source, loop);
	}

	[Command]
	void Cmd_PlaySound(AudioClip clip, GameObject source, bool loop)
	{
		Rpc_PlaySound(clip, source, loop); 
	}

	[ClientRpc]
	void Rpc_PlaySound(AudioClip clip, GameObject source, bool loop)
	{
		AudioClip lClip = loadedClips.Find( x => x == clip);
		AudioSource aSource = source.GetComponent<AudioSource>();
		aSource.clip = lClip;
		aSource.loop = loop;
		aSource.Play();
	}
}
