using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour {

	public float orthographicSize = 5f;//6f
	public float aspect = 1.33333f;//3

	// Use this for initialization
	void Start () 
	{
		//public static Matrix4x4 Ortho(float left, float right, float bottom, float top, float zNear, float zFar);


		Camera.main.projectionMatrix = Matrix4x4.Ortho(
			-orthographicSize * aspect, orthographicSize * aspect,  
			-orthographicSize, orthographicSize,
			Camera.main.nearClipPlane, Camera.main.farClipPlane);    
		
	}
}
