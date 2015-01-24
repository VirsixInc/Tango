using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	public int controllerIndex = 1;
	public string controllerName;

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

		controllerName = " Stick " + controllerIndex;
	}
	
	// Update is called once per frame	
	void Update() 
	{
		// Input
		if(Input.GetButtonDown("Flashlight" + controllerName))
		{
			// Toggle flashlight.
			Debug.Log("B Button Pressed");
		}

		if(Input.GetButtonDown("Submit" + controllerName))
		{
			// Player selects an object.
			Debug.Log("A Button Pressed");
		}

		if(Input.GetButtonDown("Pause" + controllerName))
		{
			// Player selects an object.
			Debug.Log("Pause Button Pressed");
		}

		// Movement
		if (controller.isGrounded) 
		{
			moveDirection = new Vector3(Input.GetAxis("Horizontal" + controllerName), 0, Input.GetAxis("Vertical" + controllerName));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			
			//if (Input.GetButton("Jump"))
			//moveDirection.y = jumpSpeed;			
		}
		
		if(Input.GetAxis("HorizontalRS" + controllerName) != 0 || Input.GetAxis("VerticalRS" + controllerName) != 0)
		{
			characterVisual.transform.rotation = Quaternion.LookRotation(new Vector3(Input.GetAxis("HorizontalRS" + controllerName), 0, Input.GetAxis("VerticalRS" + controllerName)));
		}
		
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}
}
