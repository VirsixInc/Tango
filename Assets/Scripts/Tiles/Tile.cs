using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour 
{
	public GameObject node;

	public bool passable;

	public bool canBeBridged;

	GameObject objectOnTile;

	TileObject tileObject;

	bool reserved;

	void Awake() 
	{
		tileObject = GetComponentInChildren<TileObject> ();
		reserved = !passable;
	}

	public bool IsReserved()
	{
		return reserved;
	}

	public void ReserveNode( bool reserve, bool canStepOn)
	{
		if(passable)
			reserved = reserve;

		if( !reserve && canStepOn && tileObject != null )
			tileObject.SteppedOff();
	}

	public Vector3 GetNodePos()
	{
		return node.transform.position;
	}

	public void SetObject( GameObject obj )
	{
		if( canBeBridged && obj != null )
		{
			//HACK this is assuming only a box can be pushed on
			//kill object
			obj.GetComponent<Box>().FallIntoHole();

			passable = true;
			canBeBridged = false;
			reserved = false;
			//TODO swap texture
		}
		else
			objectOnTile = obj;
	}

	public GameObject GetObjectOnTile()
	{
		return objectOnTile;
	}

	public void PlayerEnter(ColorComponent.pColor color)
	{
		if( tileObject != null )
			tileObject.SteppedOn(color);
	}
}
