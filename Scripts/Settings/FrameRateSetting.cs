using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Settings
{
	public class FrameRateSetting : MonoBehaviour
	{
		public int targetFrameRate = 60;

		void Awake ()
		{
			Application.targetFrameRate = targetFrameRate;
		}
	}
}
