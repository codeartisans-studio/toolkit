﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Audios
{
	[RequireComponent (typeof(AudioSource))]
	public class AudioCollision : MonoBehaviour
	{
		private AudioSource audioSource;

		void Awake ()
		{
			audioSource = GetComponent<AudioSource> ();
		}

		void OnCollisionEnter (Collision collision)
		{
			audioSource.Play ();
		}
	}
}
