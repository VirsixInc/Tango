using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	public bool activated = false;
	public bool requiresPressure = false;
	
	private Animator animator;
	private ColorComponent color;
	
	private void Start() {
		animator = GetComponent<Animator>();
		color = GetComponent<ColorComponent>();
	}
	
	public void StepOn() {
		activated = true;
//		animator.SetTrigger( "Lower" );
	}
	
	public void StepOff() {
		activated = false;
//		animator.SetTrigger( "Raise" );
	}
	
	public ColorComponent.pColor GetColor() {
		return color.currentColor;
	}
}
