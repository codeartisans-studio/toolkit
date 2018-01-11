﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Particles
{
	[AddComponentMenu ("Toolkit/Particles/ParticleEvent")]
	[RequireComponent (typeof(ParticleSystem))]
	public class ParticleEvent : MonoBehaviour
	{
		private ParticleSystem particleSystem;

		void Awake ()
		{
			particleSystem = GetComponent<ParticleSystem> ();
		}

		public void PlayParticle ()
		{
			particleSystem.Play ();
		}
	}
}
