﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Audios
{
    [AddComponentMenu("Toolkit/Audios/AudioTrigger")]
    [RequireComponent(typeof(AudioSource))]
    public class AudioTrigger : MonoBehaviour
    {
        private AudioSource audioSource;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            audioSource.Play();
        }

        void OnTriggerEnter(Collider other)
        {
            audioSource.Play();
        }
    }
}
