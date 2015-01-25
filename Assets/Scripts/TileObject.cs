using UnityEngine;
using System.Collections;

public class TileObject : MonoBehaviour 
{
	public enum Type
	{
		BUTTON,
		NONE
	}

	public Type type;
	
	public void SteppedOn(ColorComponent.pColor color)
	{
		switch(type)
		{
		case Type.BUTTON:
			ColorComponent.pColor buttonColor = gameObject.GetComponent<GameButton>().GetColor();
			if( buttonColor == color || buttonColor == ColorComponent.pColor.grey )
				gameObject.GetComponent<GameButton>().StepOn();
			break;
		case Type.NONE:
			break;
		default:
			break;
		}
	}
}
