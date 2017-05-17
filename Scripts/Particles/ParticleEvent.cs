using UnityEngine;
using System.Collections;

namespace Toolkit.Particles
{
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
