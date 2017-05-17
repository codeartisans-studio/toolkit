using UnityEngine;
using System.Collections;

namespace Toolkit.Cameras
{
	[ExecuteInEditMode]
	[RequireComponent (typeof(Camera))]
	public class CameraObliqueness : MonoBehaviour
	{
		public Vector2 obliqueness = Vector2.zero;
	
		// Use this for initialization
		void Start ()
		{
			Camera camera = GetComponent<Camera> ();

			Matrix4x4 matrix = camera.projectionMatrix;
			matrix [0, 2] = obliqueness.x;
			matrix [1, 2] = obliqueness.y;
			camera.projectionMatrix = matrix;
		}
	}
}
