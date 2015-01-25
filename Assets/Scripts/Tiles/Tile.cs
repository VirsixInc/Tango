using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour 
{
	public GameObject node;

	public bool passable;

	public bool canBeBridged;

	GameObject objectOnTile;

	bool reserved;

	void Awake() 
	{
		reserved = !passable;
	}

	public bool IsReserved()
	{
		return reserved;
	}

	public void ReserveNode( bool reserve )
	{
		if(passable)
			reserved = reserve;
	}

	public Vector3 GetNodePos()
	{
		return node.transform.position;
	}

	public void SetObject( GameObject obj )
	{
		if( canBeBridged )
		{
			//kill object
			passable = true;
			canBeBridged = false;
			//swap texture
		}
		objectOnTile = obj;
	}

	public GameObject GetObjectOnTile()
	{
		return objectOnTile;
	}
}
