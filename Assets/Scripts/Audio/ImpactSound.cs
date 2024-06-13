using System;
using UnityEngine;

namespace Audio
{
    public class ImpactSound : MonoBehaviour
    {
        private float _soundVolume;
        [SerializeField] private AudioSource audioSource;
        private const string SoundsVolumeKey = "SOUNDS_VOLUME";

        private void Start()
        {
            _soundVolume = PlayerPrefs.GetFloat(SoundsVolumeKey, audioSource.volume);
            audioSource.volume = _soundVolume;
        }

        private void OnCollisionEnter2D(Collision2D _)
        {
            audioSource.Play();
        }
    }
}
