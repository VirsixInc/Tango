using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerColor))]
public class GameButton : MonoBehaviour {

	public bool isPressed = false;
	public bool requiresPressure = false;
	public PlayerColor.pColor myColor = PlayerColor.pColor.Red;

	private void OnTriggerEnter( Collider col ) {
		if( col.transform.tag == "Player" || col.transform.tag == "Box" ) {
			PlayerColor.pColor  colColor = col.GetComponent<PlayerColor>().color;
			if( colColor == myColor )
				isPressed = true;
		}
	}

	private void OnTriggerExit( Collider col ) {
		if( isPressed || requiresPressure ) {
			isPressed = false;
		}
	}
}
