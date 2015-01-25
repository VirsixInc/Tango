using UnityEngine;
using System.Collections;

[RequireComponent (typeof (ColorComponent))]
public class Box : MonoBehaviour {

	public float pushSpeed = 6.75f;
	private bool moving;
	private TileManager tileManager;
	public Tile currentTile;
	public Tile nextTile;
	public Vector3 endPos;

	// Use this for initialization
	void Start () 
	{
		// Send Coordinates to TileManager and receive corresponding tile
		tileManager = FindObjectOfType<TileManager>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (currentTile == null)
		{
			// Find nearest x increment in .5s and y increment in 1s
			Vector3 pos = transform.position;
			
			float x = Mathf.Floor(pos.x*2f + 0.5f) / 2f;
			if( x % 1 != 0.5f ) {
				//x = ( x < pos.x ) ? x + .5f : x - .5f;
			}
			float z = Mathf.Round(pos.z);

			currentTile = tileManager.GetTileAtPosition(new Vector3(x, 0.5f, z));

			if(currentTile != null)
			{
				currentTile.SetObject(this.gameObject);
				currentTile.ReserveNode(true, false);
				transform.position = currentTile.GetNodePos();
			}
		}

		if(moving)
		{
			transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * pushSpeed);
			
			if(transform.position == endPos)
				moving = false;
		}
	}

	void FallIntoHole() 
	{
		StartCoroutine( DropAndDie() );
	}

	IEnumerator DropAndDie() 
	{
		Vector3 fromPos = transform.position;
		Vector3 toPos = new Vector3( fromPos.x, fromPos.y - 3f, fromPos.z );
		float elapsedTime = 0f;
		float lerpTime = 1f;

		while( elapsedTime < lerpTime ) 
		{
			transform.position = Vector3.Lerp( fromPos, toPos, ( elapsedTime/lerpTime ));
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		// Delete gameobject when it finishes sliding down
		gameObject.SetActive( false );
	}

	public bool MoveObject(Direction dir)
	{
		if(!moving)
		{
			nextTile = tileManager.MoveToTile(currentTile, dir, GetComponent<ColorComponent>().currentColor);
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
