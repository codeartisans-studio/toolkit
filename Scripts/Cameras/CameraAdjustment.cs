using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Cameras
{
	[AddComponentMenu ("Toolkit/Cameras/CameraAdjustment")]
	[RequireComponent (typeof(Camera))]
	public class CameraAdjustment : MonoBehaviour
	{
		public Vector2 baseResolution = new Vector2 (640f, 960f);
	
		// Use this for initialization
		void Start ()
		{
			Camera camera = GetComponent<Camera> ();
		
			float baseAspect = baseResolution.x / baseResolution.y;
		
			if (camera.aspect < baseAspect) {
				if (camera.orthographic) {
					camera.orthographicSize = camera.orthographicSize / camera.aspect * baseAspect;
				} else {
					camera.fieldOfView = Mathf.Atan ((Mathf.Tan (camera.fieldOfView / 2f * Mathf.Deg2Rad) * camera.farClipPlane / camera.aspect * baseAspect) / camera.farClipPlane) * Mathf.Rad2Deg * 2f;
				}
			}
		}
	}
}
