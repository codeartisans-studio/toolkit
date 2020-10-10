using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit
{
    [AddComponentMenu("Toolkit/Audios/AudioAnimationEvent")]
    [RequireComponent(typeof(AudioSource))]
    public class AudioAnimationEvent : MonoBehaviour
    {
        private AudioSource audioSource;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayAudio()
        {
            audioSource.Play();
        }
    }
}
