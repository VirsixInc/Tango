using UnityEngine;
using System.Collections;

[RequireComponent (typeof (ColorComponent))]
public class GameButton : MonoBehaviour {

	public bool isPressed = false;
	public bool requiresPressure = false;

	private Animator animator;
	private ColorComponent color;

	private void Start() {
		animator = GetComponent<Animator>();
		color = GetComponent<ColorComponent>();
	}

	public void StepOn() {
		isPressed = true;
		animator.SetTrigger( "Lower" );
	}

	public void StepOff() {
		if( isPressed && requiresPressure ) {
			isPressed = false;
			animator.SetTrigger( "Raise" );
		}
	}

	public ColorComponent.pColor GetColor() {
		return color.currentColor;
	}

	public void ChangeColor(ColorComponent.pColor color) {
		Color mainColor = Color.white;
		Color emitColor = Color.white;

		if (color == ColorComponent.pColor.blue) {
			mainColor = new Color(54f/255f, 64f/255f, 244f/255f);
			emitColor = Color.blue;
		} else if(color == ColorComponent.pColor.red) {
			mainColor = new Color(201f/255f, 65f/255f, 65f/255f);
			emitColor = Color.red;
		} else if(color == ColorComponent.pColor.grey) {
			mainColor = Color.white;
			emitColor = Color.grey;
		}

		transform.FindChild ("button_main").renderer.material.color = mainColor;
		transform.FindChild ("buttonBase").renderer.material.SetColor("_EmitColor", emitColor);

	}

//	private void OnTriggerEnter( Collider col ) {
//		if( col.transform.tag == "Player" || col.transform.tag == "Box" ) {
//			PlayerColor.pColor  colColor = col.GetComponent<PlayerColor>().color;
//			if( colColor == myColor ) {
//				isPressed = true;
//				animator.SetTrigger( "Depress" );
//			}
//		}
//	}
//
//	private void OnTriggerExit( Collider col ) {
//		if( isPressed && requiresPressure ) {
//			isPressed = false;
//			animator.SetTrigger( "Raise" );
//		}
//	}
}
