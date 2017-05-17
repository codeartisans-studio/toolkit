using UnityEngine;
using System.Collections;

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
