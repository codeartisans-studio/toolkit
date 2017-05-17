using UnityEngine;
using System.Collections;

namespace Toolkit.Audios
{
	[RequireComponent (typeof(AudioSource))]
	public class AudioEvent : MonoBehaviour
	{
		private AudioSource audioSource;

		void Awake ()
		{
			audioSource = GetComponent<AudioSource> ();
		}

		public void PlayAudio ()
		{
			audioSource.Play ();
		}
	}
}
