using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	public bool activated = false;
	public bool requiresPressure = false;

	private ColorComponent color;
	
	private void Start() {
		color = GetComponent<ColorComponent>();
	}
	
	public void StepOn() {
		GameManager.TeleporterOn (color.currentColor);
		activated = true;
	}
	
	public void StepOff() {
		GameManager.TeleporterOff (color.currentColor);
		activated = false;
	}

//	public void Teleport() {
//
//	}
	
	public ColorComponent.pColor GetColor() {
		return color.currentColor;
	}
}
