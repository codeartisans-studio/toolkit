using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
