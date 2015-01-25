using UnityEngine;
using System.Collections;

public class ForceField : MonoBehaviour {

	void Start () {
		TurnOn ();
	}
	
	void Update () {
	
	}

	public void TurnOn() {
		GetComponentInParent<Tile> ().ReserveNode (true, false);
		renderer.enabled = true;
	}

	public void TurnOff() {
		GetComponentInParent<Tile> ().ReserveNode (false, false);
		renderer.enabled = false;
	}

	public void ChangeColor(ColorComponent.pColor color) {
		Renderer[] renderers = GetComponentsInChildren<Renderer> ();
		
		foreach(Renderer rend in renderers) {
			if (color == ColorComponent.pColor.blue) {
				rend.material.color = new Color(0f, 0f, 1f, .7f);
			} else if(color == ColorComponent.pColor.red) {
				rend.material.color = new Color(1f, 0f, 0f, .7f);
			} else if(color == ColorComponent.pColor.grey) {
				Debug.Log("No gray force fields", this);
			}
		}
	}
}
