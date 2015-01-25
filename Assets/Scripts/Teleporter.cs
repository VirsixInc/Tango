using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	public bool activated = false;

	private ColorComponent color;

	ForceField forceField;

	AudioSource myAudioSource;
	
	private void Start() {
		color = GetComponent<ColorComponent>();
		forceField = transform.parent.FindChild ("Force Field").GetComponent<ForceField> ();
		myAudioSource = GetComponent<AudioSource>();
	}
	
	public void StepOn() {
		GameManager.TeleporterOn (color.currentColor);
		activated = true;
	}
	
	public void StepOff() {
		GameManager.TeleporterOff (color.currentColor);
		activated = false;
	}

	public void ChangeColor(ColorComponent.pColor color) {
		Renderer[] renderers = GetComponentsInChildren<Renderer> ();

		foreach(Renderer rend in renderers) {
			if (color == ColorComponent.pColor.blue) {
				rend.material.color = new Color(54f/255f, 64f/255f, 244f/255f);
			} else if(color == ColorComponent.pColor.red) {
				rend.material.color = new Color(201f/255f, 65f/255f, 65f/255f);
			} else if(color == ColorComponent.pColor.grey) {
				Debug.Log("No gray teleporters", this);
			}
		}
	}
	
	public ColorComponent.pColor GetColor() {
		return color.currentColor;
	}

	public void PlayEndOfLevelSound() {
		myAudioSource.Play();
	}
}
