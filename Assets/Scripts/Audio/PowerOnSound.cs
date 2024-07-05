using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class PowerOnSound : MonoBehaviour
    {
        [SerializeField] private AudioClip clipOn;
        [SerializeField] private AudioClip clipOff;
        [SerializeField] private AudioSource audioSource;

        private void OnEnable()
        {
            audioSource.clip = clipOn;
            audioSource.Play();
        }
        
        private void OnDisable()
        {
            audioSource.clip = clipOff;
            audioSource.Play();
        }
    }
}
