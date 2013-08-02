using UnityEngine;
using System.Collections;

[AddComponentMenu("Helper/Play Clip At Point")]
public class PlayClipAtPoint : Entity {
	public AudioController audioController;
	public bool playing = false;
	float life;
	public void Update () {
		if (/*!destroy && */playing && !audio.isPlaying/* && isMine*/)
			//RPCMessage (MessageUtility.destroy);
			Destroy (gameObject, 1.0f);
	}
	/*
	#region Network View
	[RPC]
	new public void Message (string message) {
		string[] messages = message.Split (' ');
		if (messages.Length == 0)
			return;
		
		string order = messages [0];
		
		if (MessageUtility.SetPlayClipAtPoint (order)) {
			audio.rolloffMode = (AudioRolloffMode) int.Parse (messages [1]);
			audio.clip = prefabManager.GetAudioClipSet (StringUtility.NetworkUnpack (messages [2])).RandomObject () as AudioClip;
			audio.maxDistance = float.Parse (messages [3]);
			audio.Play ();
			playing = true;
		} else {
			base.Message (message);
		}
	}
	#endregion*/
}
