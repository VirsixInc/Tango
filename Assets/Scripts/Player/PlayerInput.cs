using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	public int controllerIndex = 1;
	public string controllerName;

	CharacterController controller;
	
	public float speed = 6.0F;
	
	public float gravity = 20.0F;
	//private Vector3 moveDirection = Vector3.zero;
	
	public GameObject characterVisual;

	// Grid Movement
	private bool moving;
	private TileManager tileManager;
	public Tile currentTile;
	public Tile nextTile;
	public Vector3 endPos;
	private Direction currentDirection;

	// Grabbing a box
	private bool isGrabbing;
	public GameObject grabbedGameObject;
	public Box grabbedBoxComponent;
	//player color
	ColorComponent.pColor color;


	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<CharacterController>();

		controllerName = " Stick " + controllerIndex;

		GameObject tileHolders = GameObject.Find("Tile Holders");
		if(tileHolders != null)
		{
			tileManager = tileHolders.GetComponent<TileManager>();
		}

		currentDirection = Direction.UP;
		isGrabbing = false;

		color = gameObject.GetComponent<ColorComponent>().currentColor;

		if (currentTile == null)
		{
			currentTile = tileManager.GetTileFromIndex(1);
			transform.position = new Vector3(currentTile.GetNodePos().x, transform.position.y, currentTile.GetNodePos().z);
		}
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

			grabbedGameObject = tileManager.GrabObjectAtTile(currentTile, currentDirection);
			
			if(grabbedGameObject != null)
			{
				if(grabbedGameObject.GetComponent<ColorComponent>() != null 
				   && grabbedGameObject.GetComponent<ColorComponent>().currentColor == color)
				{
					Debug.Log("Grabbing box!");
					grabbedBoxComponent = grabbedGameObject.GetComponent<Box>();
					isGrabbing = true;
				}
			}
		}
		else if(Input.GetButtonUp("Submit" + controllerName))
		{
			Debug.Log("A Button Released");
			isGrabbing = false;
			grabbedGameObject = null;
			grabbedBoxComponent = null;
		}

		if(Input.GetButtonDown("Pause" + controllerName))
		{
			// Player selects an object.
			Debug.Log("Pause Button Pressed");
		}

		// Movement
		if (moving && (transform.position == endPos))
			moving = false;

		if(isGrabbing)
		{
			if(currentDirection == Direction.UP || currentDirection == Direction.DOWN)
			{
				if(!moving && Input.GetAxis("Vertical" + controllerName) > 0)
				{
					if(currentDirection == Direction.UP)
						grabbedBoxComponent.MoveObject(currentDirection);

					nextTile = tileManager.MoveToTile(currentTile, Direction.UP, color);
					SetDestinationTile();

					if(currentDirection != Direction.UP)
						grabbedBoxComponent.MoveObject(Direction.UP);
				}
				
				if(!moving && Input.GetAxis("Vertical" + controllerName) < 0)
				{
					if(currentDirection == Direction.DOWN)
						grabbedBoxComponent.MoveObject(Direction.DOWN);

					nextTile = tileManager.MoveToTile(currentTile, Direction.DOWN, color);
					SetDestinationTile();

					if(currentDirection != Direction.DOWN)
						grabbedBoxComponent.MoveObject(Direction.DOWN);

				}
			}
			else if(currentDirection == Direction.LEFT || currentDirection == Direction.RIGHT)
			{
				if(!moving && Input.GetAxis("Horizontal" + controllerName) < 0)
				{
					if(currentDirection == Direction.LEFT)
						grabbedBoxComponent.MoveObject(Direction.LEFT);

					nextTile = tileManager.MoveToTile(currentTile, Direction.LEFT, color);
					SetDestinationTile();

					if(currentDirection != Direction.LEFT)
						grabbedBoxComponent.MoveObject(Direction.LEFT);

				}
				
				if(!moving && Input.GetAxis("Horizontal" + controllerName) > 0)
				{
					if(currentDirection == Direction.RIGHT)
						grabbedBoxComponent.MoveObject(Direction.RIGHT);

					nextTile = tileManager.MoveToTile(currentTile, Direction.RIGHT, color);
					SetDestinationTile();

					if(currentDirection != Direction.RIGHT)
						grabbedBoxComponent.MoveObject(Direction.RIGHT);

				}
			}
		}
		else
		{
			if(!moving && Input.GetAxis("Vertical" + controllerName) > 0)
			{
				nextTile = tileManager.MoveToTile(currentTile, Direction.UP, color);
				currentDirection = Direction.UP;
				SetDestinationTile();
			}
			
			if(!moving && Input.GetAxis("Vertical" + controllerName) < 0)
			{
				nextTile = tileManager.MoveToTile(currentTile, Direction.DOWN, color);
				currentDirection = Direction.DOWN;
				SetDestinationTile();
			}
			
			if(!moving && Input.GetAxis("Horizontal" + controllerName) < 0)
			{
				nextTile = tileManager.MoveToTile(currentTile, Direction.LEFT, color);
				currentDirection = Direction.LEFT;
				SetDestinationTile();
			}
			
			if(!moving && Input.GetAxis("Horizontal" + controllerName) > 0)
			{
				nextTile = tileManager.MoveToTile(currentTile, Direction.RIGHT, color);
				currentDirection = Direction.RIGHT;
				SetDestinationTile();
			}
		}
		switch(currentDirection)
		{
		case Direction.UP:
			characterVisual.transform.rotation = Quaternion.LookRotation(Vector3.forward);
			break;
		case Direction.DOWN:
			characterVisual.transform.rotation = Quaternion.LookRotation(Vector3.back);
			break;
		case Direction.LEFT:
			characterVisual.transform.rotation = Quaternion.LookRotation(Vector3.left);
			break;
		case Direction.RIGHT:
			characterVisual.transform.rotation = Quaternion.LookRotation(Vector3.right);
			break;
		}

		if(moving)
		{
			// find the target position relative to the player:
			Vector3 dir = endPos - transform.position;
			// calculate movement at the desired speed:
			Vector3 movement = dir.normalized * speed * Time.deltaTime;
			// limit movement to never pass the target position:
			if (movement.magnitude > dir.magnitude) 
			{
				movement = dir;
				moving = false;
			}

			//movement.y -= gravity * Time.deltaTime;

			controller.Move(movement);
		}
	}

	public void ChangeColor(ColorComponent.pColor color) {
		Renderer[] renderers = GetComponentsInChildren<Renderer> ();
		
		foreach(Renderer rend in renderers) {
			if (color == ColorComponent.pColor.blue) {
				rend.material.SetColor("_MainColor", Color.blue);
				rend.material.SetColor("_EmitColor", Color.red);
			} else if(color == ColorComponent.pColor.red) {
				rend.material.SetColor("_MainColor", Color.red);
				rend.material.SetColor("_EmitColor", Color.blue);
			} else if(color == ColorComponent.pColor.grey) {
				Debug.Log("No gray teleporters", this);
			}
		}
	}

	void SetDestinationTile()
	{
		if (nextTile != null && currentTile != nextTile)
		{
			currentTile.ReserveNode(false, true);
			currentTile = nextTile;
			moving = true;
			endPos = nextTile.GetNodePos();
		}
	}
}
