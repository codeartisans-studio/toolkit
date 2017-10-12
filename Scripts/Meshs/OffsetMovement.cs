using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Meshs
{
	public class OffsetMovement : MonoBehaviour
	{
		public Vector2 speed = Vector2.left;

		private MeshRenderer mashRenderer;

		void Awake ()
		{
			mashRenderer = GetComponent<MeshRenderer> ();
		}

		// Update is called once per frame
		void Update ()
		{
			Vector2 offset = -speed * Time.time;
			mashRenderer.material.SetTextureOffset ("_MainTex", offset);
		}
	}
}
