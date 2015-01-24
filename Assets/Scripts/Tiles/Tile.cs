using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour 
{
	public GameObject node;

	bool reserved;

	void Awake() 
	{
		reserved = false;
	}

	public bool IsReserved()
	{
		return reserved;
	}

	public void ReserveNode( bool reserve )
	{
		reserved = reserve;
	}

	public Vector3 GetNodePos()
	{
		return node.transform.position;
	}
}
