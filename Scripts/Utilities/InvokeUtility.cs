using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Utilities
{
	public static class InvokeUtility
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
