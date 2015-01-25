using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Direction {
	UP, DOWN, LEFT, RIGHT
}

public class TileManager : MonoBehaviour 
{
	const int width = 16, height = 9;
	Tile[] tiles;

	public GameObject defaultTile;

	void Awake () 
	{
		tiles = new Tile[width*height];
		GameObject[] holders = GameObject.FindGameObjectsWithTag("TileHolders");// transform.GetComponentsInChildren<Transform>();

		for(int i = 0; i < holders.Length; i++) 
		{
			int y = (int)char.GetNumericValue(holders[i].name[0]);
			int x;
			if( holders[i].name.Length > 3 )
				x = (int)char.GetNumericValue(holders[i].name[3]) + 10;
			else
				x = (int)char.GetNumericValue(holders[i].name[2]);

			if(holders[i].transform.childCount == 0) 
			{
				GameObject newTile = (GameObject)Instantiate(defaultTile);
				newTile.transform.parent = holders[i].transform;
				newTile.transform.localPosition = Vector3.zero;
				tiles[y*width + x] = newTile.GetComponent<Tile>();
			} 
			else 
			{
				tiles[y*width + x] = holders[i].transform.GetChild(0).GetComponent<Tile>();
			}
		}
	}

	/// <summary>
	/// Returns tile if empty and reserves it. Returns null if not empty or doesnt exist. Does NOT unreserve current. Fix if you want
	/// </summary>
	/// <returns>The to tile.</returns>
	/// <param name="tile">Tile.</param>
	/// <param name="direction">Direction.</param>
	public Tile MoveToTile( Tile tile, Direction direction, ColorComponent.pColor color )
	{
		int index = GetIndex(tile);
		int targetIndex = 0;
		switch(direction) 
		{
		case Direction.UP:
			targetIndex = index - width;

			if(targetIndex > -1) 
			{
				if(!tiles[targetIndex].IsReserved()) 
				{
					tiles[targetIndex].ReserveNode(true, true);
					tiles[targetIndex].PlayerEnter(color);
					return tiles[targetIndex];
				}
			}
			break;
		case Direction.DOWN:
			targetIndex = index + width;

			if(targetIndex < (width * height) ) 
			{
				if(!tiles[targetIndex].IsReserved()) 
				{
					tiles[targetIndex].ReserveNode(true, true);
					tiles[targetIndex].PlayerEnter(color);
					return tiles[targetIndex];
				}
			}
			break;
		case Direction.LEFT:
			targetIndex = (index % width) - 1;

			if(targetIndex > -1) 
			{
				if(!tiles[index - 1].IsReserved()) 
				{
					tiles[index - 1].ReserveNode(true, true);
					tiles[index - 1].PlayerEnter(color);
					return tiles[index - 1];
				}
			}
			break;
		case Direction.RIGHT:
			targetIndex = (index % width) + 1;

			if(targetIndex < width) 
			{
				if(!tiles[index + 1].IsReserved()) 
				{
					tiles[index + 1].ReserveNode(true, true);
					tiles[index + 1].PlayerEnter(color);
					return tiles[index + 1];
				}
			}
			break;
		default:
			return null;
		}
		return null;
	}

	public Tile MoveObjectToTile( Tile tile, Direction direction, ColorComponent.pColor color )
	{
		int index = GetIndex(tile);
		int targetIndex = 0;
		switch(direction) 
		{
		case Direction.UP:
			targetIndex = index - width;
			
			if(targetIndex > -1) 
			{
				if(!tiles[targetIndex].IsReserved() || tiles[targetIndex].canBeBridged) 
				{
					tiles[targetIndex].ReserveNode(true, true);
					tiles[targetIndex].PlayerEnter(color);
					return tiles[targetIndex];
				}
			}
			break;
		case Direction.DOWN:
			targetIndex = index + width;
			
			if(targetIndex < (width * height) ) 
			{
				if(!tiles[targetIndex].IsReserved() || tiles[targetIndex].canBeBridged) 
				{
					tiles[targetIndex].ReserveNode(true, true);
					tiles[targetIndex].PlayerEnter(color);
					return tiles[targetIndex];
				}
			}
			break;
		case Direction.LEFT:
			targetIndex = (index % width) - 1;
			
			if(targetIndex > -1) 
			{
				if(!tiles[index - 1].IsReserved() || tiles[targetIndex].canBeBridged) 
				{
					tiles[index - 1].ReserveNode(true, true);
					tiles[index - 1].PlayerEnter(color);
					return tiles[index - 1];
				}
			}
			break;
		case Direction.RIGHT:
			targetIndex = (index % width) + 1;
			
			if(targetIndex < width) 
			{
				if(!tiles[index + 1].IsReserved() || tiles[targetIndex].canBeBridged) 
				{
					tiles[index + 1].ReserveNode(true, true);
					tiles[index + 1].PlayerEnter(color);
					return tiles[index + 1];
				}
			}
			break;
		default:
			return null;
		}
		return null;
	}

	public GameObject GrabObjectAtTile( Tile tile, Direction direction )
	{
		int index = GetIndex(tile);
		int targetIndex = 0;
		switch(direction) 
		{
		case Direction.UP:
			targetIndex = index - width;
			
			if(targetIndex > -1) 
			{
				return tiles[targetIndex].GetObjectOnTile();
			}
			break;
		case Direction.DOWN:
			targetIndex = index + width;
			
			if(targetIndex < (width * height)) 
			{
				return tiles[targetIndex].GetObjectOnTile();
			}
			break;
		case Direction.LEFT:
			targetIndex = (index % width) - 1;
			
			if(targetIndex > -1) 
			{
				return tiles[index - 1].GetObjectOnTile();
			}
			break;
		case Direction.RIGHT:
			targetIndex = (index % width) + 1;
			
			if(targetIndex < width) 
			{
				return tiles[index + 1].GetObjectOnTile();
			}
			break;
		default:
			return null;
		}
		return null;
	}

	int GetIndex(Tile tile) 
	{
		for(int i = 0; i < tiles.Length; i++) 
		{
			if(tile == tiles[i])
				return i;
		}
		return -1;
	}

	public Tile GetTileFromIndex(int index) 
	{
		return tiles[index];
	}

	public Tile GetTileAtPosition( Vector3 pos )
	{
		foreach( Tile tile in tiles )
		{
			if( pos.x >= tile.transform.position.x - 0.2f && pos.x <= tile.transform.position.x + 0.2f
			   && pos.z >= tile.transform.position.z - 0.2f && pos.z <= tile.transform.position.z + 0.2f )
			{
				return tile;
			}
		}
		return null;
	}
}
