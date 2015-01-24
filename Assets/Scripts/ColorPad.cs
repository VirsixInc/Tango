using UnityEngine;
using System.Collections;

public class ColorPad : MonoBehaviour 
{
	enum colors
	{
		grey = 0,
		blue = 1,			
		red = 2
	};

	int color;

	// Use this for initialization
	void Start () 
	{
		color = (int)colors.grey;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter(Collision other)
	{
		//If collision is a player
		if(other.gameObject.tag == "Player")
		{
			//Switch Colors with the colliding player
			//int t_color = (int)colors[color];
			//color = colors.other.gameObject.GetComponent(Player).color;
			//other.gameObject.GetComponent(Player).color = colors.t_color;
		}
	}

	//Alternate system incase the players don't actually collide with the pad, but are standing above it
	void MyCollision()
	{
		RaycastHit hit;
		if(Physics.CapsuleCast(transform.position + Vector3.down, transform.position, 1.0f, Vector3.up, out hit))
		{
			//If collision is a player
			if(hit.collider.tag == "Player")
			{
				//Switch Colors with the colliding player
				//int t_color = colors[color];
				//color = colors[hit.collider.gameObject.GetComponent(Player).color];
				//hit.collider.gameObject.GetComponent(Player).color = colors[t_color];
			}
		}
	}

	//If one of the players is grey, make the pads the color which is not in use between the players
	void ForceMissingColor()
	{
		/*
		//If one of the players is grey
		if(player1.GetComponent(Player).color == colors.grey)
		{
			//Check player2's color and set the pad color to the other
			int t_color = colors[player2.GetComponent(Player).color];
		}
		if(player2.GetComponent(Player).color == colors.grey)
		{
			//Check player1's color and set the pad color to the other
			int t_color = colors[player1.GetComponent(Player).color];
			if(t_color == colors.red)
				color = colors.blue;
			else if(t_color == colors.blue)
				color = colors.red;
		}
		*/
	}

}
