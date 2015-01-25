using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour {

	public delegate void EffectVolumeAction();
	public static event EffectVolumeAction ModifyEffectVolume;

	public delegate void MusicVolumeAction();
	public static event MusicVolumeAction ModifyMusicVolume;


	void Start () {
	
	}
	
	void Update () {
	
	}

	public void UpdateEffectsVolume(float volume) {
		GameManager.effectsVolume = (int)volume;
		transform.FindChild("Volume").FindChild ("Sound Effects").GetComponent<Text> ().text = "Sound Effects: " + (int)volume;

		if( ModifyEffectVolume != null ) {
			ModifyEffectVolume();
		}
	}

	public void UpdateMusicVolume(float volume) {
		GameManager.musicVolume = (int)volume;
		transform.FindChild("Volume").FindChild ("Music").GetComponent<Text> ().text = "Music: " + (int)volume;

		if( ModifyMusicVolume != null ) {
			ModifyMusicVolume();
		}
	}
}
