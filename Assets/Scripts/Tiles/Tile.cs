using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour 
{
	public GameObject node;

	public bool passable;

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
		if( passable )
			reserved = reserve;
	}

	public Vector3 GetNodePos()
	{
		return node.transform.position;
	}
}
