using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Transforms
{
	[AddComponentMenu ("Toolkit/Transforms/FollowTarget")]
	public class FollowTarget : MonoBehaviour
	{
		public Transform target;
		public Vector3 offset;

		private void LateUpdate ()
		{
			transform.position = target.position + offset;
		}
	}
}
