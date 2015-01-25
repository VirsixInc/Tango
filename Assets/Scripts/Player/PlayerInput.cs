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
	}
	
	// Update is called once per frame	
	void Update() 
	{
		// Debug
		if (currentTile == null)
		{
			currentTile = tileManager.GetTileFromIndex(1);
			this.transform.position = new Vector3(currentTile.GetNodePos().x, this.transform.position.y, currentTile.GetNodePos().z);
		}
		else
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
				isGrabbing = true;
			}
			else if(Input.GetButtonUp("Submit" + controllerName))
			{
				Debug.Log("A Button Released");
				isGrabbing = false;
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
						nextTile = tileManager.MoveToTile(currentTile, Direction.UP, color);
						SetDestinationTile();
					}
					
					if(!moving && Input.GetAxis("Vertical" + controllerName) < 0)
					{
						nextTile = tileManager.MoveToTile(currentTile, Direction.DOWN, color);
						SetDestinationTile();
					}
				}
				else if(currentDirection == Direction.LEFT || currentDirection == Direction.RIGHT)
				{
					if(!moving && Input.GetAxis("Horizontal" + controllerName) < 0)
					{
						nextTile = tileManager.MoveToTile(currentTile, Direction.LEFT, color);
						SetDestinationTile();
					}
					
					if(!moving && Input.GetAxis("Horizontal" + controllerName) > 0)
					{
						nextTile = tileManager.MoveToTile(currentTile, Direction.RIGHT, color);
						SetDestinationTile();
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
