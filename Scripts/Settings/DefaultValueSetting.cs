using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Toolkit.Settings
{
	[AddComponentMenu ("Toolkit/Settings/DefaultValueSetting")]
	public class DefaultValueSetting : MonoBehaviour
	{
		public float defaultTimeScale = 1f;
		public AudioMixerSnapshot defaultSnapshot;

		void Awake ()
		{
			// pause in game will change time scale, load new scene will not be revert automatic
			Time.timeScale = defaultTimeScale;

			// pause in game will change snapshot, load new scene will not revert as default
			defaultSnapshot.TransitionTo (0f);
		}
	}
}
