using UnityEngine;
using System.Collections;

[AddComponentMenu("Helper/Audio Controller")]
public class AudioController : MonoBehaviour {
	public float soundLevel;
	public AudioSourceType audioSourceType;
	
	void Update () {
		switch (audioSourceType) {
			case AudioSourceType.Game:
				audio.volume = soundLevel * PreferenceUtility.GameVolume;
				break;
			case AudioSourceType.Ambience:
				audio.volume = soundLevel * PreferenceUtility.AmbienceVolume;
				break;
			case AudioSourceType.Music:
				audio.volume = soundLevel * PreferenceUtility.MusicVolume;
				break;
		}
	}
}

public enum AudioSourceType {
	Game,
	Ambience,
	Music
}
