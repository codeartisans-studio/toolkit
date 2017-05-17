using UnityEngine;
using System;
using System.Collections;

namespace Toolkit.Utils
{
	public static class InvokeUtil
	{
		public static IEnumerator WaitForFixedUpdate (Action action)
		{
			yield return new WaitForFixedUpdate ();

			action ();
		}

		public static IEnumerator WaitForNextFrame (Action action)
		{
			yield return null;

			action ();
		}

		public static IEnumerator WaitForSeconds (Action action, float seconds)
		{
			yield return new WaitForSeconds (seconds);

			action ();
		}

		public static IEnumerator WaitForEndOfFrame (Action action)
		{
			yield return new WaitForEndOfFrame ();

			action ();
		}
	}
}
