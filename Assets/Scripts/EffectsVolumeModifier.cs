using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class EffectsVolumeModifier : MonoBehaviour {

	AudioSource myAudioSource;

	// Use this for initialization
	void Start () {
		myAudioSource = GetComponent<AudioSource>();

		if( myAudioSource == null ) {
			Debug.LogWarning( gameObject.name + "'s AudioModifier is missing it's AudioSource" );
		}
	}

	void OnEnable() {
		OptionMenu.ModifyEffectVolume += UpdateEffectVolume;
	}

	void OnDisable() {
		OptionMenu.ModifyEffectVolume -= UpdateEffectVolume;
	}

	void UpdateEffectVolume() {
		if( myAudioSource != null ) {
			myAudioSource.volume = GameManager.effectsVolume / 10f;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
