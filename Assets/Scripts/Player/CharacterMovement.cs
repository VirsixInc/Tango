using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	CharacterController controller;

	public float speed = 6.0F;

	// No jumping for now, keeping this just in case.
	//public float jumpSpeed = 8.0F;

	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;

	public GameObject characterVisual;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame	
	void Update() 
	{
		if (controller.isGrounded) 
		{
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;

			//if (Input.GetButton("Jump"))
				//moveDirection.y = jumpSpeed;
			
		}

		if(Input.GetAxis("HorizontalRS") != 0 || Input.GetAxis("VerticalRS") != 0)
		{
			characterVisual.transform.rotation = Quaternion.LookRotation(new Vector3(Input.GetAxis("HorizontalRS"), 0, Input.GetAxis("VerticalRS")));
		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}
}
