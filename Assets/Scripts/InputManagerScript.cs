﻿using UnityEngine;
using System.Collections.Generic;

public class InputManagerScript : MonoBehaviour {
	/*
	 * Probably don't use this script, use InputManager's static methods instead
	 */

	public bool ignoreInput;
	public bool snapshotShortcut = true;
	private const KeyCode SNAPSHOT_KEY = KeyCode.P;
	//private KeyCode[] ACTION_KEYS = {KeyCode.Space, KeyCode.LeftControl, KeyCode.Z};
	//private KeyCode[] DIRECTION_SWITCH_KEYS = {KeyCode.Q};
	private const KeyCode HINT_KEY = KeyCode.H;
	private const KeyCode RESET_KEY = KeyCode.R;

	List<Button> _inputs;
	List<Button> _lastInputs;

	
	void Start () {
		ignoreInput = false;
		_inputs = new List<Button>();
		_lastInputs = new List<Button>();
	}

	void LateUpdate () {
		// Update variables
		_lastInputs = _inputs;
		_inputs = new List<Button>();

		// Input
		if (snapshotShortcut) {
			if (Input.GetKeyDown (SNAPSHOT_KEY)) {
				string fname = "Assets/Snapshots/tmp/Screenshot" + System.DateTime.Now.ToString ("MM:DD hh:mm:ss.fff") + ".png";
				Debug.Log ("Snapshot saved: " + fname);
				Application.CaptureScreenshot (fname);
			}
		}

		float hIn = Input.GetAxis("Horizontal");
		float vIn = Input.GetAxis("Vertical");
		
		// How far does the stick have to be pressed to be considered a direction
		float threshhold = .5f;

		// Horizontal/Vertical directions of input
		if(hIn > threshhold)
			_inputs.Add(Button.RIGHT);
		else if (hIn < -threshhold)
			_inputs.Add(Button.LEFT);

		if(vIn > threshhold)
			_inputs.Add(Button.UP);
		else if (vIn < -threshhold)
			_inputs.Add(Button.DOWN);

		if(true){};

		if(Input.GetButtonDown("Activate")){
			_inputs.Add(Button.ACTION);
		}


		if (Input.GetKey (HINT_KEY)) {
			Globals.latentHintManager.resetTimer();
			_inputs.Add(Button.HINT);
			//Debug.Log("HINT");
		}

		
		if (Input.GetKey (RESET_KEY)) {
			_inputs.Add(Button.RESET);
		}
		//TO REMOVE DIRECTION_SWITCH_KEYS
		/*
		foreach(KeyCode kc in DIRECTION_SWITCH_KEYS){
			if(Input.GetKeyDown(kc)){
				_inputs.Add(Button.DIRECTION_SWITCH);
			}
		}
		*/

		if(ignoreInput){
			_inputs.Clear();
		}

	}

	Button DirectionToButton(Direction d){
		switch(d){
		case Direction.UP:
			return Button.UP;
		case Direction.LEFT:
			return Button.LEFT;
		case Direction.RIGHT:
			return Button.RIGHT;
		case Direction.DOWN:
			return Button.DOWN;
		default:
			return Button.NONE;
		}
	}

	public bool GetButton(Button button){
		if(_inputs.Contains(button))
			return true;
		else 
			return false;
	}

	public bool GetButtonDown(Button button){
		if(_inputs.Contains(button) && !_lastInputs.Contains(button))
			return true;
		else 
			return false;
	}

	public bool GetButtonStay(Button button){
		if(_inputs.Contains(button) && _lastInputs.Contains(button))
			return true;
		else 
			return false;
	}

	public bool GetDirection(Direction direction){
		Button button = DirectionToButton(direction);
		if(_inputs.Contains(button))
			return true;
		else 
			return false;
	}
	
	public bool GetDirectionDown(Direction direction){
		Button button = DirectionToButton(direction);
		if(_inputs.Contains(button) && !_lastInputs.Contains(button))
			return true;
		else 
			return false;
	}
	
	public bool GetDirectionStay(Direction direction){
		Button button = DirectionToButton(direction);
		if(_inputs.Contains(button) && _lastInputs.Contains(button))
			return true;
		else 
			return false;
	}
}
