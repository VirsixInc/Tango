using UnityEngine;
using System.Collections;

public class MovableObject : MonoBehaviour 
{
	public float speed = 6.0f;
	private bool moving;
	private TileManager tileManager;
	public Tile currentTile;
	public Tile nextTile;
	public Vector3 endPos;

	// Use this for initialization
	void Start () 
	{
		GameObject tileHolders = GameObject.Find("Tile Holders");
		if(tileHolders != null)
		{
			tileManager = tileHolders.GetComponent<TileManager>();
		}

		moving = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(moving)
		{
			transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * speed);

			if(transform.position == endPos)
				moving = false;
		}
	}

	public bool MoveObject(Direction dir)
	{
		if(!moving)
		{
			//nextTile = tileManager.MoveToTile(currentTile, dir);
			//currentDirection = Direction.UP;
			return SetDestinationTile();
		}

		return false;
	}

	bool SetDestinationTile()
	{
		if (nextTile != null && currentTile != nextTile)
		{
			currentTile.ReserveNode(false, false);
			currentTile.SetObject(null);
			currentTile = nextTile;
			currentTile.SetObject(this.gameObject);
			moving = true;
			endPos = nextTile.GetNodePos();

			return true;
		}

		return false;
	}
}
