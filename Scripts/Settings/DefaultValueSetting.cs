using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

namespace Toolkit.Settings
{
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
