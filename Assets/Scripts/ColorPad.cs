using UnityEngine;
using System.Collections;

public class ColorPad : MonoBehaviour 
{
	public enum pColor
	{
		grey = 0,
		blue = 1,
		red = 2
	}

	public pColor color;

	// Use this for initialization
	void Start () 
	{
		color = pColor.grey;
	}
	
	// Update is called once per frame
	void Update () 
	{
		ForceMissingColor ();
		SetMaterialColor ();
	}

	void OnCollisionEnter(Collision other)
	{
		//If collision is a player
		if(other.gameObject.tag == "Player")
		{
			//Switch color with the colliding player's color
			pColor t_color = color;
			//color = other.gameObject.GetComponent(Player).color;
			//other.gameObject.GetComponent(Player).color = t_color;
		}
	}

	//Alternate system incase the players don't actually collide with the pad, but are standing above it
	void MyCollision()
	{
		RaycastHit hit;
		if(Physics.CapsuleCast(transform.position + Vector3.down, transform.position, 1.0f, Vector3.up, out hit, 1.0f))
		{
			//If collision is a player
			if(hit.collider.tag == "Player")
			{
				//Switch color with the colliding player
				pColor t_color = color;
				//color = hit.collider.gameObject.GetComponent(Player).color;
				//hit.collider.gameObject.GetComponent(Player).color = t_color;
			}
		}
	}

	//If one of the players is grey, make the pads the color which is not in use between the players
	void ForceMissingColor()
	{
		/*
		//If one of the players is grey
		if(player1.GetComponent(Player).color == pColor.grey)
		{
			//Check player2's color and set the pad color to the other
			pColor t_color = player2.GetComponent(Player).color;
		}
		if(player2.GetComponent(Player).color == pColor.grey)
		{
			//Check player1's color and set the pad color to the other
			pColor t_color = player1.GetComponent(Player).color;
			if(t_color == pColor.red)
				color = pColor.blue;
			else if(t_color == pColor.blue)
				color = pColor.red;
		}
		*/
	}

	void SetMaterialColor()
	{
		if (color == pColor.blue)
			renderer.material.color = Color.blue;
		else if (color == pColor.red)
			renderer.material.color = Color.red;
		else
			renderer.material.color = Color.white;
	}

}
