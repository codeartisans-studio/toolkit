﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolkit.Structs;
using Toolkit.Attributes;
using Random = UnityEngine.Random;

namespace Toolkit.Audios
{
	[CreateAssetMenu (menuName = "Audio Events/RandomAudioEvent")]
	public class RandomAudioEvent : AudioEvent
	{
		public AudioClip[] clips;

		public Range volume = new Range (1f, 1f);
		public Range pitch = new Range (1f, 1f);

		public override void Play (AudioSource source)
		{
			if (clips.Length == 0)
				return;

			source.clip = clips [Random.Range (0, clips.Length)];
			source.volume = Random.Range (volume.minValue, volume.maxValue);
			source.pitch = Random.Range (pitch.minValue, pitch.maxValue);
			source.Play ();
		}
	}
}
