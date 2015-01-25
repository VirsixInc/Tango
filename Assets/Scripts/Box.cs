using UnityEngine;
using System.Collections;

[RequireComponent (typeof (ColorComponent))]
public class Box : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Find nearest x increment in .5s and y increment in 1s
		Vector3 pos = transform.position;

		float x = Mathf.Floor(pos.x*2f + 0.5f) / 2f;
		if( x % 1 != 0.5f ) {
			x = ( x < pos.x ) ? x + .5f : x - .5f;
		}
		float y = Mathf.Round(pos.z);

		// Send Coordinates to TileManager and receive corresponding tile
		TileManager tM = FindObjectOfType<TileManager>();
		//tM GetTileAtPosition
		//tile.ReserveNode(true);
		//transform.position = tile.GetNodePos();
		//tile.SetObject( gameobject );
	}

	void FallIntoHole() {
		StartCoroutine( DropAndDie() );
	}

	IEnumerator DropAndDie() {
		Vector3 fromPos = transform.position;
		Vector3 toPos = new Vector3( fromPos.x, fromPos.y - 3f, fromPos.z );
		float elapsedTime = 0f;
		float lerpTime = 1f;

		while( elapsedTime < lerpTime ) {
			transform.position = Vector3.Lerp( fromPos, toPos, ( elapsedTime/lerpTime ));
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		// Delete gameobject when it finishes sliding down
		gameObject.SetActive( false );
	}
}
