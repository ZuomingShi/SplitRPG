﻿using UnityEngine;
using System.Collections;

public static class Utils {

	public const float TILED_TO_UNITY_SCALE = 0.015623f;
	// Declaration of a simple delegate type
	public delegate void VoidDelegate();

	public static Vector2 ComponentMultiply(Vector2 a, Vector2 b) {
		return new Vector2(a.x * b.x, a.y * b.y);
	}

	public static HeightScript GetHeightScript(GameObject o){
		HeightScript hs = o.GetComponent<HeightScript>();
		if(hs == null){
			hs = o.AddComponent<HeightScript>();
		}
		return hs;
	}

	public static GameObject Create(GameObject _go, float _x, float _y){
		return (GameObject)(Object.Instantiate (_go, new Vector2 (_x, _y), Quaternion.identity));
	}

	/// <summary>
	/// Are the values equal within the tolerance
	/// </summary>
	public static bool CloseValues(float v1, float v2, float tolerance){
		if(Mathf.Abs(v1 - v2) < tolerance)
			return true;
		else 
			return false;
	}

	/// <summary>
	/// Move a value towards a destination value by speed.
	/// </summary>
	public static float MoveValueTowards(float value, float dest, float speed){
		if(Utils.CloseValues(value, dest, speed +.000001f)){
			return dest;
		} else if (value < dest){
			value = value + speed;
			if(value >= dest)
				return dest;
		} else if (value > dest){
			value = value - speed;
			if(value <= dest)
				return dest;
		}
		return value;
	}

	/// <summary>
	/// Prints out an error message if a statement fails.
	/// </summary>
	public static bool assert(bool statement, string context = ""){
		if (!statement) {
			Debug.LogError("Statement failed: " + context);
		}
		return statement;
	}

	/// <summary>
	/// Clamp the specified value to between min and max.
	/// </summary>
	public static float Clamp(float value, float min, float max){
		if(value <= min)
			return min;
		else if (value >= max)
			return max;
		else
			return value;
	}

	/// <summary>
	/// Is the layer's name equal to 'name'?
	/// </summary>
	public static bool LayerIs(int layer, string name){
		if(LayerMask.NameToLayer(name) == layer){
			return true;
		} else {
			return false;
		}
	}

	/// <summary>
	/// Returns the player on the layer passed in (left or right)
	/// </summary>
	public static PlayerControllerScript PlayerOnLayer(int layer){
		PlayerControllerScript player = (layer == LayerMask.NameToLayer("Right") ? Globals.playerRight : Globals.playerLeft);
		//Debug.Log ("PlayerOnLayer Called");
		return player;
	}

	/// <summary>
	/// Convert pixels to tiles
	/// </summary>
	public static int PixelsToTiles(int val){
		return val / Globals.PIXELS_PER_TILE;
	}

	/// <summary>
	/// Returns a vector that points in the specified direction e.g. Left -> (-1, 0)
	/// </summary>
	public static Vector2 DirectionToVector(Direction d){
		switch(d){
		case Direction.LEFT:
			return new Vector2(-1, 0);
		case Direction.RIGHT:
			return new Vector2(1, 0);
		case Direction.UP:
			return new Vector2(0, 1);
		case Direction.DOWN:
			return new Vector2(0, -1);
		default:
			return new Vector2(0, 0);
		}
	}

	/// <summary>
	/// Returns a new GameObject with the script as a component.
	/// </summary>
	public static T CreateScript<T>() where T: Component {
		T o = new GameObject().AddComponent<T>();
		o.name = o.GetType().Name;
		return o;
	}

	/// <summary>
	/// Round the specified value.
	/// </summary>
	public static int Round(float value){
		return Mathf.FloorToInt(value + .5f);
	}

	/// <summary>
	/// Finds the child anywhere under an object
	/// </summary>
	public static Transform FindChildRecursive(GameObject obj, string child){
		Transform o = obj.transform.FindChild(child);
		if(o != null){
			return o;
		} else {
			foreach(Transform t in obj.transform){
				//Transform o2 = t.transform.FindChild(child);
				Transform o2 = Utils.FindChildRecursive(t.gameObject, child);// t.transform.FindChild(child);
				if(o2 != null)
					return o2;
			}
		}
		return null;
	}
}
