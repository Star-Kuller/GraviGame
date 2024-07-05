using System;
using Services.EventBus;
using Services.ServiceLocator;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class PowerOnSound : MonoBehaviour
    {
        [SerializeField] private AudioClip clipOn;
        [SerializeField] private AudioClip clipOff;
        [SerializeField] private AudioSource audioSource;

        private void Start()
        {
            var services = ServiceLocator.Current;
            services.Get<EventBus>().Subscribe(EventList.ShowPauseButton, PlayOff);
        }

        private void OnEnable()
        {
            audioSource.clip = clipOn;
            audioSource.Play();
        }
        
        private void PlayOff()
        {
            audioSource.clip = clipOff;
            audioSource.Play();
        }
    }
}
