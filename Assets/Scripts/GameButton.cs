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
