using UnityEngine;
using System.Collections;

namespace Toolkit.Transforms
{
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
