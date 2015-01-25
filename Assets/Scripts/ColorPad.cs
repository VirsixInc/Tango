using UnityEngine;
using System.Collections;

public class ColorPad : MonoBehaviour 
{
	ColorComponent color;

	// Use this for initialization
	void Start () 
	{
		color = gameObject.GetComponent<ColorComponent> ();
		color.currentColor = ColorComponent.pColor.grey;
	}
	
	// Update is called once per frame
	void Update () 
	{
		ForceMissingColor ();
		SetMaterialColor ();
	}

	void OnTriggerEnter(Collider other)
	{
		//If collision is a player
		if(other.gameObject.tag == "Player")
		{
			//Switch color with the colliding player's color
			ColorComponent.pColor t_color = color.currentColor;
			color.currentColor = other.gameObject.GetComponentInChildren<ColorComponent>().currentColor;
			other.gameObject.GetComponentInChildren<ColorComponent>().currentColor = t_color;
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
				ColorComponent.pColor t_color = color.currentColor;
				color.currentColor = hit.collider.gameObject.GetComponentInChildren<ColorComponent>().currentColor;
				hit.collider.gameObject.GetComponentInChildren<ColorComponent>().currentColor = t_color;
			}
		}
	}

	//If one of the players is grey, make the pads the color which is not in use between the players
	//This may change/be removed
	void ForceMissingColor()
	{
		//Not sure how tile objects reference the player, so commented out for now...
		/*
		//If one of the players is grey
		if(player1.GetComponentInChildren<ColorComponent>().currentColor == ColorComponent.pColor.grey)
		{
			//Check player2's color and set the pad color to the other
			ColorComponent.pColor t_color = player2.GetComponentInChildren<ColorComponent>().currentColor;
		}
		if(player2.GetComponentInChildren<ColorComponent>().currentColor == ColorComponent.pColor.grey)
		{
			//Check player1's color and set the pad color to the other
			ColorComponent.pColor t_color = player1.GetComponentInChildren<ColorComponent>().currentColor;
			if(t_color == ColorComponent.pColor.red)
				color.currentColor = ColorComponent.pColor.blue;
			else if(t_color == ColorComponent.pColor.blue)
				color.currentColor = ColorComponent.pColor.red;
		}
		*/
	}

	void SetMaterialColor()
	{
		if (color.currentColor == ColorComponent.pColor.blue)
			renderer.material.color = Color.blue;
		else if (color.currentColor == ColorComponent.pColor.red)
			renderer.material.color = Color.red;
		else
			renderer.material.color = Color.white;
	}

}
