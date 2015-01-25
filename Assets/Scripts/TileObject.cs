using UnityEngine;
using System.Collections;

public class TileObject : MonoBehaviour 
{
	public enum Type
	{
		BUTTON,
		TELEPORTER,
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
		case Type.TELEPORTER:
			ColorComponent.pColor teleporterColor = gameObject.GetComponent<Teleporter>().GetColor();
			if(teleporterColor == color)
				gameObject.GetComponent<Teleporter>().StepOn();
			break;
		case Type.NONE:
			break;
		default:
			break;
		}
	}

	public void SteppedOff() {
		switch(type)
		{
		case Type.BUTTON:
//			ColorComponent.pColor buttonColor = gameObject.GetComponent<GameButton>().GetColor();
//			if( buttonColor == color || buttonColor == ColorComponent.pColor.grey )
			gameObject.GetComponent<GameButton>().StepOff();
			break;
		case Type.TELEPORTER:
//			ColorComponent.pColor teleporterColor = gameObject.GetComponent<Teleporter>().GetColor();
//			if(teleporterColor == color)
			gameObject.GetComponent<Teleporter>().StepOff();
			break;
		case Type.NONE:
			break;
		default:
			break;
		}
	}
}
