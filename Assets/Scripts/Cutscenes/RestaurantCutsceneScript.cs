﻿using UnityEngine;
using System.Collections;

public class RestaurantCutsceneScript : CutsceneScript {
	
	[Tooltip("All wait times are some multiple of this.")]
	public float standardBubbleDisplayTime = 2.0f;

	public GameObject plateWithChickenBubble;
	public GameObject thumbsUpBubble;
	public GameObject smileyFaceBubbleRightTail;
	public GameObject smileyFaceBubbleLeftTail;
	public GameObject exclaimationMarkBubbleRightTail;
	public GameObject exclaimationMarkBubbleLeftTail;
	public GameObject necklaceBubble;
	public GameObject jumpingBubble;
	public GameObject elipsesBubbleLeftTail;
	public GameObject braceletBubble;
	public GameObject pushingBlocksBubble;
	public GameObject videoGamesBubble;

	protected override IEnumerator ActionSequence() {
		float waitTime = 0;
		GameObject b;
		
		CheckPrefabLinks();

		SetupScene();

		PlayAnimation(leftPlayer, "SitRight");
		PlayAnimation(rightPlayer, "SitLeft");

		yield return new WaitForSeconds(standardBubbleDisplayTime * 2.0f);

		// Girl says "Good chicken"
		b = ShowSpeechBubble(leftPlayer, plateWithChickenBubble);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 1.5f);

		HideSpeechBubble(b);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 8.0f);

		b = ShowSpeechBubble(leftPlayer, thumbsUpBubble);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 1.5f);

		HideSpeechBubble(b);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 2.0f);

		// Boy says "I agree"
		b = ShowSpeechBubble(rightPlayer, smileyFaceBubbleRightTail);
		yield return new WaitForSeconds(standardBubbleDisplayTime);
		
		HideSpeechBubble(b);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 2.0f);

		// They stand up infront of the table.
		Move(rightPlayer, Direction.LEFT, 0);
		Move(leftPlayer, Direction.RIGHT, 0);
		yield return new WaitForSeconds(standardBubbleDisplayTime);

		// Boy says "I got you a present"
		b = ShowSpeechBubble(rightPlayer, exclaimationMarkBubbleRightTail);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 1.5f);

		HideSpeechBubble(b);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 8.0f);

		b = ShowSpeechBubble(rightPlayer, necklaceBubble);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 1.5f);

		HideSpeechBubble(b);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 2.0f);

		
		PlayAnimation(leftPlayer, "WalkDownAnimation");
		PlayAnimation(rightPlayer, "WalkDownAnimation");

		waitTime = Move(leftPlayer, Direction.DOWN);
		waitTime = Move(rightPlayer, Direction.DOWN);
		yield return new WaitForSeconds(waitTime);
		
		waitTime = Move(leftPlayer, Direction.RIGHT);
		waitTime = Move(rightPlayer, Direction.LEFT);
		yield return new WaitForSeconds(waitTime + standardBubbleDisplayTime / 2.0f);

		// Boy gives present to girl.
		// Stuff?
		// Boy says "It's for jumping"
		b = ShowSpeechBubble(rightPlayer, jumpingBubble);
		yield return new WaitForSeconds(standardBubbleDisplayTime);

		HideSpeechBubble(b);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 4.0f);

		// Girl thanks boy and gives bracelet
		b = ShowSpeechBubble(leftPlayer, exclaimationMarkBubbleLeftTail);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 1.5f);

		HideSpeechBubble(b);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 8.0f);

		b = ShowSpeechBubble(leftPlayer, smileyFaceBubbleLeftTail);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 1.5f);

		HideSpeechBubble(b);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 2.0f);

		b = ShowSpeechBubble(leftPlayer, exclaimationMarkBubbleLeftTail);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 1.5f);

		HideSpeechBubble(b);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 8.0f);

		b = ShowSpeechBubble(leftPlayer, elipsesBubbleLeftTail);
		yield return new WaitForSeconds(standardBubbleDisplayTime);

		HideSpeechBubble(b);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 8.0f);

		b = ShowSpeechBubble(leftPlayer, braceletBubble);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 1.5f);

		HideSpeechBubble(b);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 2.0f);

		// Girl takes off bracelet and gives to guy.
		// Stuff?
		// Girl says "It's for heavy things"
		b = ShowSpeechBubble(leftPlayer, pushingBlocksBubble);
		yield return new WaitForSeconds(standardBubbleDisplayTime);

		HideSpeechBubble(b);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 2.0f);

		// Boy thanks girl
		b = ShowSpeechBubble(rightPlayer, exclaimationMarkBubbleRightTail);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 1.5f);

		HideSpeechBubble(b);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 8.0f);
		
		b = ShowSpeechBubble(rightPlayer, smileyFaceBubbleRightTail);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 1.5f);

		// Long pause
		HideSpeechBubble(b);
		yield return new WaitForSeconds(standardBubbleDisplayTime * 2.0f);

		// Girl says "Want to play video games?"
		b = ShowSpeechBubble(leftPlayer, videoGamesBubble);
		yield return new WaitForSeconds(standardBubbleDisplayTime);
		
		HideSpeechBubble(b);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 2.0f);

		// Boy says "Yep"
		b = ShowSpeechBubble(rightPlayer, smileyFaceBubbleRightTail);
		yield return new WaitForSeconds(standardBubbleDisplayTime);
		
		HideSpeechBubble(b);
		yield return new WaitForSeconds(standardBubbleDisplayTime / 2.0f);

		float tmp = rightCamera.fader.fadeRate;
		rightCamera.fader.fadeRate = fadeRate;
		leftCamera.fader.fadeRate = fadeRate;
		FadeCameraOut(leftCamera);
		waitTime = FadeCameraOut(rightCamera);

		yield return new WaitForSeconds(waitTime);

		TearDownScene();
		leftPlayer.GetComponent<CharacterMovementScript>().canJump = true;
		rightPlayer.GetComponent<CharacterMovementScript>().canPushHeavy = true;

		Move(leftPlayer, Direction.UP, 0);
		Move(rightPlayer, Direction.UP, 0);

		FadeCameraIn(leftCamera);
		waitTime = FadeCameraIn(rightCamera);
		yield return new WaitForSeconds(waitTime);

		rightCamera.fader.fadeRate = tmp;
		leftCamera.fader.fadeRate = tmp;

		End();
	}

	private bool CheckPrefabLinks() {
		if(plateWithChickenBubble == null
		   || thumbsUpBubble == null
		   || smileyFaceBubbleLeftTail == null
		   || smileyFaceBubbleRightTail == null
		   || exclaimationMarkBubbleLeftTail == null
		   || exclaimationMarkBubbleRightTail == null
		   || necklaceBubble == null
		   || jumpingBubble == null
		   || elipsesBubbleLeftTail == null
		   || braceletBubble == null
		   || pushingBlocksBubble == null
		   || videoGamesBubble == null
		   ) {
			Debug.LogError("Missing speech bubble assignment.");
			return false;
		}
		return true;
	}
}
