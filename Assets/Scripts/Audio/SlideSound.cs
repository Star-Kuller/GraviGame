using System;
using Services.EventBus;
using Services.ServiceLocator;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class SlideSound : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] private float volume = 1;
        [Range(0, 3)]
        [SerializeField] private float pitch = 1;
        [SerializeField] private AudioClip clip;
        [SerializeField] private AudioMixerGroup audioMixerGroup;
        private AudioSource _audioSource;
        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = clip;
            _audioSource.outputAudioMixerGroup = audioMixerGroup;
            _audioSource.volume = volume;
            _audioSource.pitch = pitch;
            _audioSource.playOnAwake = false;
            _audioSource.loop = true;
            var services = ServiceLocator.Current;
            var eventBus = services.Get<EventBus>();
            eventBus.Subscribe(EventList.Pause, _audioSource.Pause);
            eventBus.Subscribe(EventList.Victory, _audioSource.Pause);
            eventBus.Subscribe(EventList.Loose, _audioSource.Pause);
            eventBus.Subscribe(EventList.Resume, _audioSource.UnPause);
        }
        
        private void OnCollisionEnter2D(Collision2D _)
        {
            _audioSource.Play();
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            _audioSource.volume = Mathf.Clamp(other.relativeVelocity.magnitude / 3 , 0, 1) * 0.5f;
        }

        private void OnCollisionExit2D(Collision2D _)
        {
            _audioSource.Stop();
        }
    }
}
