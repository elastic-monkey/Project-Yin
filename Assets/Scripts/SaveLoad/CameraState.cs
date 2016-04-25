using UnityEngine;
using System.Collections;

[System.Serializable]
public class CameraState {
	public SerializableVector3 position;

	public CameraState(){
		GameObject camera = GameObject.Find ("Camera Pivot");
		position = camera.transform.position;
	}
}
