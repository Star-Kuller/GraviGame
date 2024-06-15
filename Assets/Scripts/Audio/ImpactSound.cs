using System;
using UnityEngine;

namespace Audio
{
    public class ImpactSound : MonoBehaviour
    {
        private float _soundVolume;
        [SerializeField] private AudioSource audioSource;
        private Rigidbody2D _rigidbody;
        private Vector2 _lastVelocity = new Vector2(0, 0);
        private const string SoundsVolumeKey = "SOUNDS_VOLUME";

        private void Start()
        {
            _soundVolume = PlayerPrefs.GetFloat(SoundsVolumeKey, audioSource.volume);
            audioSource.volume = _soundVolume;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D _)
        {
            audioSource.Play();
        }

        private void OnCollisionStay2D(Collision2D _)
        {
            if(Math.Abs(_lastVelocity.magnitude - _rigidbody.velocity.magnitude) > 0.5)
                audioSource.Play();
            _lastVelocity = _rigidbody.velocity;
        }
    }
}
