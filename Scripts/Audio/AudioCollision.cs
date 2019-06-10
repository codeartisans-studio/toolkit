using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Audios
{
    [AddComponentMenu("Toolkit/Audios/AudioCollision")]
    [RequireComponent(typeof(AudioSource))]
    public class AudioCollision : MonoBehaviour
    {
        private AudioSource audioSource;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            audioSource.Play();
        }

        void OnCollisionEnter(Collision collision)
        {
            audioSource.Play();
        }
    }
}
