using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Utility/SoundUtility")]
public class SoundUtility : Utility {
	public static void SpawnPlayClipAtPoint (AudioClip ac, Vector3 position, float distance = 30.0f, AudioRolloffMode audioRolloffMode = AudioRolloffMode.Custom) {
		//if (!ValidAudioClipSet (audioClipSetName))
		//	return;
		
		PlayClipAtPoint playClipAtPoint = (Instantiate (managerPrefab.playClipAtPoint, position, Quaternion.identity) as GameObject).GetComponent<PlayClipAtPoint> ();
		
		playClipAtPoint.audio.rolloffMode = audioRolloffMode;
		playClipAtPoint.audio.clip = ac;//prefabManager.GetAudioClipSet (audioClipSetName).RandomObject () as AudioClip;
		playClipAtPoint.audio.maxDistance = distance;
		playClipAtPoint.audio.Play ();
		playClipAtPoint.playing = true;
	}
	
	/*
	public static void NetworkSpawnPlayClipAtPoint (string audioClipSetName, Vector3 position, float distance = 30.0f, AudioRolloffMode audioRolloffMode = AudioRolloffMode.Custom) {
		if (!ValidAudioClipSet (audioClipSetName))
			return;
		
		PlayClipAtPoint playClipAtPoint = (NetworkUtility.NetworkInstantiate (Linker.prefabManager.playClipAtPoint, position, Quaternion.identity)).GetComponent<PlayClipAtPoint> ();
		
		playClipAtPoint.RPCMessage (MessageUtility.setPlayClipAtPoint 
			+ " " + ((int) audioRolloffMode) 
			+ " " + StringUtility.NetworkPack (audioClipSetName) 
			+ " " + distance);
	}
	
	public static bool ValidAudioClipSet (string audioClipSetName) {
		return prefabManager.GetAudioClipSet (audioClipSetName) != null;
	}
	*/
}
