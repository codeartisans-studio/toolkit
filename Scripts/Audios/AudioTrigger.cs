using UnityEngine;
using System.Collections;

namespace Toolkit.Audios
{
	[RequireComponent (typeof(AudioSource))]
	public class AudioTrigger : MonoBehaviour
	{
		private AudioSource audioSource;

		void Awake ()
		{
			audioSource = GetComponent<AudioSource> ();
		}

		void OnTriggerEnter2D (Collider2D other)
		{
			audioSource.Play ();
		}
	}
}
