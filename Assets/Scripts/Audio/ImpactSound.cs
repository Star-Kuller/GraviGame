using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class ImpactSound : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] private float volume = 1;
        [Range(0, 3)]
        [SerializeField] private float pitch = 1;
        [SerializeField] private AudioClip clip;
        [SerializeField] private AudioMixerGroup audioMixerGroup;
        private AudioSource _audioSource;
        private Rigidbody2D _rigidbody;
        private Vector2 _lastVelocity = new Vector2(0, 0);
        
        private float ImpactVolume => Mathf.Clamp(
            (_lastVelocity.magnitude - _rigidbody.velocity.magnitude) / 3f,
            0, 1);

        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = clip;
            _audioSource.outputAudioMixerGroup = audioMixerGroup;
            _audioSource.volume = volume;
            _audioSource.pitch = pitch;
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D _)
        {
            _audioSource.volume = ImpactVolume;
            _audioSource.Play();
        }

        private void OnCollisionStay2D(Collision2D _)
        {
            if (_lastVelocity.magnitude - _rigidbody.velocity.magnitude > 0.5f)
            {
                _audioSource.volume = ImpactVolume;
                _audioSource.Play();
            }
            _lastVelocity = _rigidbody.velocity;
        }
    }
}
