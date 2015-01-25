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

	public void StepOn(ColorComponent.pColor otherColor)
	{
		//Switch color with the colliding player's color
		ColorComponent.pColor t_color = color.currentColor;
		color.currentColor = otherColor;
		//player.GetComponentInChildren<ColorComponent>().currentColor = t_color
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
