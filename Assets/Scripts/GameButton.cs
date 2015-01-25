using UnityEngine;
using System.Collections;

[RequireComponent (typeof (ColorComponent))]
public class GameButton : MonoBehaviour {

	public bool isPressed = false;
	public bool requiresPressure = false;

	private AudioSource myAudioSource;
	private Animator animator;
	private ColorComponent color;

	public ForceField target;

	private void Start() {
		animator = GetComponent<Animator>();
		color = GetComponent<ColorComponent>();
		if(target == null)
			Debug.Log("Hook up target", this);

		myAudioSource = GetComponent<AudioSource>();
	}

	public void StepOn() {
		myAudioSource.Play();

		isPressed = true;
		animator.SetTrigger( "Lower" );
		if(target != null)
			target.TurnOff();
	}

	public void StepOff() {
		if( isPressed && requiresPressure ) {
			myAudioSource.Play();

			isPressed = false;
			animator.SetTrigger( "Raise" );
			if(target != null)
				target.TurnOn();
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
	
	void OnDrawGizmosSelected() {
		if(target != null)
			Gizmos.DrawLine (transform.position, target.transform.position);
	}
}
