using UnityEngine;
using System;
using System.Collections;

namespace Toolkit.UI
{
	[RequireComponent (typeof(Canvas))]
	public class CanvasVisibility : MonoBehaviour
	{
		public GameObject board;

		private Action action;

		public void Show (Action callback)
		{
			Show ();

			action = callback;
		}

		public void Show ()
		{
			board.SetActive (true);
		}

		public void Hide ()
		{
			board.SetActive (false);
		}

		public void Back ()
		{
			if (action != null) {
				action ();

				action = null;
			}
		}
	}
}
