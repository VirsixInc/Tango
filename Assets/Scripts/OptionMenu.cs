using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour {
	
	void Start () {
	
	}
	
	void Update () {
	
	}

	public void UpdateEffectsVolume(float volume) {
		GameManager.effectsVolume = (int)volume;
		transform.FindChild("Volume").FindChild ("Sound Effects").GetComponent<Text> ().text = "Sound Effects: " + (int)volume;
	}

	public void UpdateMusicVolume(float volume) {
		GameManager.musicVolume = (int)volume;
		transform.FindChild("Volume").FindChild ("Music").GetComponent<Text> ().text = "Music: " + (int)volume;
	}
}
