﻿using UnityEngine;
using System.Collections;

public class CameraFadeTransitionAction : Action {

	CameraScript cameraScript;
	Utils.VoidDelegate middleCode;

	public static CameraFadeTransitionAction Create(CameraScript cam, Utils.VoidDelegate middleCode, Utils.VoidDelegate callback){
		CameraFadeTransitionAction a = Utils.CreateScript<CameraFadeTransitionAction>();
		a.cameraScript = cam;
		a.middleCode = middleCode;
		a.AddDelegate(callback);
		return a;
	}

	public override void OnStartAction(){
		cameraScript.fader.FadeDown(Middle);
	}

	void Middle(){
		if(middleCode != null)
			middleCode();
		cameraScript.fader.FadeUp(Finish);
	}

}