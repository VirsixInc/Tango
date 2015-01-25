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
	private bool dying;

	// Use this for initialization
	void Start () 
	{
		// Send Coordinates to TileManager and receive corresponding tile
		tileManager = FindObjectOfType<TileManager>();
		dying = false;

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
				currentTile.ReserveNode(true, true);
				transform.position = currentTile.GetNodePos();
			}
		}

		if(moving)
		{
			transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * pushSpeed);
			
			if(transform.position == endPos)
			{
				currentTile.SetObject(this.gameObject);
				moving = false;
			}
		}
	}

	public void FallIntoHole() 
	{
		dying = true;
		StartCoroutine( DropAndDie() );
	}

	public void ChangeColor(ColorComponent.pColor color) {
		Renderer[] renderers = GetComponentsInChildren<Renderer> ();
		
		foreach(Renderer rend in renderers) {
			if (color == ColorComponent.pColor.blue) {
				rend.material.color = new Color(54f/255f, 64f/255f, 244f/255f);
			} else if(color == ColorComponent.pColor.red) {
				rend.material.color = new Color(201f/255f, 65f/255f, 65f/255f);
			} else if(color == ColorComponent.pColor.grey) {
				Debug.Log("No gray teleporters", this);
			}
		}
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
		if(!moving && ! dying)
		{
			nextTile = tileManager.MoveObjectToTile(currentTile, dir, GetComponent<ColorComponent>().currentColor);
			//currentDirection = Direction.UP;
			return SetDestinationTile();
		}
		
		return false;
	}
	
	bool SetDestinationTile()
	{
		if (nextTile != null && currentTile != nextTile)
		{
			currentTile.ReserveNode(false, true);
			currentTile.SetObject(null);
			currentTile = nextTile;
			//currentTile.SetObject(this.gameObject);
			moving = true;
			endPos = nextTile.GetNodePos();
			
			return true;
		}
		
		return false;
	}

	public bool IsDying()
	{
		return dying;
	}
}
