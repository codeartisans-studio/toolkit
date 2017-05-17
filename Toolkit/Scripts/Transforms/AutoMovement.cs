﻿using UnityEngine;
using System.Collections;

namespace Toolkit.Transforms
{
	public class AutoMovement : MonoBehaviour
	{
		public Vector3 speed = Vector3.left;
		public Space space = Space.Self;
	
		// Update is called once per frame
		void Update ()
		{
			transform.Translate (speed * Time.deltaTime, space);
		}
	}
}
