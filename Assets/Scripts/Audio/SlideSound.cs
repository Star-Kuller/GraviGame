using System;
using Services.EventBus;
using Services.ServiceLocator;
using UnityEngine;

namespace Audio
{
    public class SlideSound : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        private const string SoundsVolumeKey = "SOUNDS_VOLUME";
        private void Start()
        {
            var services = ServiceLocator.Current;
            var eventBus = services.Get<EventBus>();
            eventBus.Subscribe(EventList.Pause, Pause);
            eventBus.Subscribe(EventList.Victory, Pause);
            eventBus.Subscribe(EventList.Loose, Pause);
            eventBus.Subscribe(EventList.Resume, UnPause);
        }

        private void Pause()
        {
            audioSource.Pause();
        }
        
        private void UnPause()
        {
            audioSource.UnPause();
        }

        private void OnCollisionEnter2D(Collision2D _)
        {
            audioSource.Play();
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            audioSource.volume = Mathf.Clamp(other.relativeVelocity.magnitude / 3 , 0, 1) * 0.5f;
        }

        private void OnCollisionExit2D(Collision2D _)
        {
            audioSource.Stop();
        }
    }
}
