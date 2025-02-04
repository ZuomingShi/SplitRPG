using UnityEngine;
using System.Collections;

public class RoomManagerScript : MonoBehaviour {

	public bool fadeTransition = false;
	public Utils.VoidDelegate transitionDelegate;

	CameraScript leftCamera, rightCamera;
	PlayerControllerScript leftPlayer, rightPlayer;

	BoxCollider2D roomColliderLeft, roomColliderLeftPrev;
	BoxCollider2D roomColliderRight, roomColliderRightPrev;
	private int roomRightLayer;
	private int roomLeftLayer;

	bool needsTransition;

	public Room leftRoom {get; set;}
	public Room rightRoom {get; set;}

	// Track how many cameras have finished their transition
	int cameraFinishes;

	void Start () {
		leftCamera = Globals.cameraLeft;
		rightCamera = Globals.cameraRight;

		leftPlayer = Globals.playerLeft;
		rightPlayer = Globals.playerRight;

		needsTransition = false;
		cameraFinishes = 0;

		leftRoom = new Room();
		rightRoom = new Room();

		roomLeftLayer = LayerMask.NameToLayer("RoomsLeft");
		roomRightLayer = LayerMask.NameToLayer("RoomsRight");
	}

	public int PlayerLayerToRoomLayer(int playerLayer) {
		if(playerLayer == Globals.playerLeft.gameObject.layer) {
			return roomLeftLayer;
		}
		if(playerLayer == Globals.playerRight.gameObject.layer) {
			return roomRightLayer;
		}

		Debug.LogError("Not a player layer: " + playerLayer);
		return -1;
	}

	public BoxCollider2D GetRoomAtPoint(Vector2 point, int layer) {
		return (BoxCollider2D) Physics2D.OverlapPoint(point, 1 << layer);
	}

	void Update() {
		if(needsTransition){
			BeginCameraTransition();
			needsTransition = false;
		}
	
		roomColliderLeft = GetRoomAtPoint(leftPlayer.xy, roomLeftLayer);
		roomColliderRight = GetRoomAtPoint(rightPlayer.xy, roomRightLayer);

		if (roomColliderLeft != roomColliderLeftPrev || roomColliderRight != roomColliderRightPrev) {
			if(roomColliderLeft != null && roomColliderRight != null){
				MoveScreen();
			}
		}

		roomColliderLeftPrev = roomColliderLeft;
		roomColliderRightPrev = roomColliderRight;
	}

	public Room GetRoom(int layer){
		if(LayerMask.NameToLayer("Left") == layer){
			return leftRoom;
		} else {
			return rightRoom;
		}
	}

	public void MoveCamerasToPoint(Vector2 point) {
		Update();
		needsTransition = false;

		RecalculateRoomBounds();

		leftCamera.transform.position = new Vector3(point.x, point.y, leftCamera.z);
		rightCamera.transform.position = new Vector3(point.x, point.y, rightCamera.z);
	}

	/// <summary>
	/// Call when the screen needs to be moved to another room
	/// </summary>
	void MoveScreen(){
		// Need to wait a short bit before transitioning 
		// because one character will get stuck if he hasn't moved yet
		needsTransition = true;
	}

	private void RecalculateRoomBounds(){
		int left, top, width, height;

		CalculateRoomBounds(roomColliderLeft, out left, out top, out width, out height);
		leftRoom.SetRect(left, top, width, height);

		CalculateRoomBounds(roomColliderRight, out left, out top, out width, out height);
		rightRoom.SetRect(left, top, width, height);
	}

	private void CalculateRoomBounds(BoxCollider2D roomCollider, out int left, out int top, out int width, out int height) {
		left = Mathf.RoundToInt (roomCollider.transform.position.x);
		top = Mathf.RoundToInt (roomCollider.transform.position.y);
		width = Utils.PixelsToTiles (Mathf.RoundToInt (roomCollider.size.x));
		height = Utils.PixelsToTiles (Mathf.RoundToInt (roomCollider.size.y));
	}

	/// <summary>
	/// Resets the bounds of the room, moves the cameras, disables movement for players
	/// </summary>
	void BeginCameraTransition(){
		RecalculateRoomBounds();

		// Pan the cameras
		if(!fadeTransition) {
			leftCamera.BeginNewRoomTransitionPan(CameraTransitionFinished);
			rightCamera.BeginNewRoomTransitionPan(CameraTransitionFinished);
		}
		else {
			leftCamera.BeginRoomTransitionFade(CameraTransitionFinished);
			rightCamera.BeginRoomTransitionFade(CameraTransitionFinished);
		}

		// Disable player movement
		leftPlayer.DisableMovement();
		rightPlayer.DisableMovement();
	}

	void SaveCheckpoint() {
		Globals.levelManager.SaveCheckpoint();
	}

	void EndCameraTransition(){
		leftPlayer.EnableMovement();
		rightPlayer.EnableMovement();
		if(transitionDelegate != null)
			transitionDelegate();
	}

	// Wait for two cameras to finish, then enable player input
	void CameraTransitionFinished(){
		cameraFinishes++;
		if(cameraFinishes == 2){
			cameraFinishes = 0;
			if(!fadeTransition) {
				SaveCheckpoint();
			}
			EndCameraTransition();
		}
	}


}
