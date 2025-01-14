﻿using UnityEngine;
using System.Collections;

public class Globals {
	
	/// <summary>
	/// Width of one side in tiles
	/// </summary>
	public const int SIDEWIDTH = 18;

	/// <summary>
	/// Height of one side in tiles
	/// </summary>
	public const int SIDEHEIGHT = 20;

	/// <summary>
	/// Number of pixels per tile (assuming you imported from Tiled)
	/// </summary>
	public const int PIXELS_PER_TILE = 64;
	
	public static int fireLevel = 3;
	public static bool justReloaded = false;
	public static bool fire1toggled = false;
	public static bool fire2toggled = false;
	public static bool fire3toggled = false;
	public static bool readyForTeleport = false;

	static PlayerControllerScript _playerLeft = null;
	public static PlayerControllerScript playerLeft{
		get{
			if(_playerLeft == null)
				_playerLeft = GameObject.Find("PlayerLeft").GetComponent<PlayerControllerScript>();
			return _playerLeft;
		}
	}

	static PlayerControllerScript _playerRight = null;
	public static PlayerControllerScript playerRight{
		get{
			if(_playerRight == null)
				_playerRight = GameObject.Find("PlayerRight").GetComponent<PlayerControllerScript>();
			return _playerRight;
		}
	}

	static CameraScript _cameraLeft = null;
	public static CameraScript cameraLeft{
		get{
			if(_cameraLeft == null)
				_cameraLeft = GameObject.Find("CameraLeft").GetComponent<CameraScript>();
			return _cameraLeft;
		}
	}
	
	static CameraScript _cameraRight = null;
	public static CameraScript cameraRight{
		get{
			if(_cameraRight == null)
				_cameraRight = GameObject.Find("CameraRight").GetComponent<CameraScript>();
			return _cameraRight;
		}
	}

	static GameManagerScript _gameManager = null;
	public static GameManagerScript gameManager{
		get{
			if(_gameManager == null)
				_gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
			return _gameManager;
		}
	}

	static RoomManagerScript _roomManager = null;
	public static RoomManagerScript roomManager{
		get{
			if(_roomManager == null)
				_roomManager = gameManager.GetComponent<RoomManagerScript>();
			return _roomManager;
		}
	}

	static HeightSorterScript _heightSorter = null;
	public static HeightSorterScript heightSorter{
		get{
			if(_heightSorter == null)
				_heightSorter = gameManager.GetComponent<HeightSorterScript>();
			return _heightSorter;
		}
	}

	static CollisionManagerScript _collisionManager = null;
	public static CollisionManagerScript collisionManager{
		get{
			if(_collisionManager == null)
				_collisionManager = gameManager.GetComponent<CollisionManagerScript>();
			return _collisionManager;
		}
	}

	static SoundManagerScript _soundManager = null;
	public static SoundManagerScript soundManager {
		get{
			if(_soundManager == null)
				_soundManager = gameManager.GetComponent<SoundManagerScript>();
			return _soundManager;
		}
	}

	static LevelManagerScript _levelManager = null;
	public static LevelManagerScript levelManager {
		get{
			if(_levelManager == null)
				_levelManager = gameManager.GetComponent<LevelManagerScript>();
			return _levelManager;
		}
	}

	
	static LatentHintManager _latentHintManager = null;
	public static LatentHintManager latentHintManager {
		get{
			if(_latentHintManager == null)
				_latentHintManager = gameManager.GetComponent<LatentHintManager>();
			return _latentHintManager;
		}
	}
	
	static OptionsMenuScript _OptionsManager = null;
	public static OptionsMenuScript OptionsManager {
		get{
			if(_OptionsManager == null)
				_OptionsManager = gameManager.GetComponent<OptionsMenuScript>();
			return _OptionsManager;
		}
	}

	static LineScript _lineScript = null;
	public static LineScript line {
		get {
			if(_lineScript == null)
				_lineScript = gameManager.transform.Find("LineCamera").Find("Line").GetComponent<LineScript>();
			return _lineScript;
		}
	}

}