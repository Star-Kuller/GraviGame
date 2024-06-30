using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class ImpactSound : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        private Rigidbody2D _rigidbody;
        private Vector2 _lastVelocity = new Vector2(0, 0);

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D _)
        {
            audioSource.volume = Mathf.Clamp(
                                     (_lastVelocity.magnitude - _rigidbody.velocity.magnitude) / 2.5f
                                     , 0, 1);
            audioSource.Play();
        }

        private void OnCollisionStay2D(Collision2D _)
        {
            if (_lastVelocity.magnitude - _rigidbody.velocity.magnitude > 0.5f)
            {
                audioSource.volume = Mathf.Clamp(
                                         (_lastVelocity.magnitude - _rigidbody.velocity.magnitude) / 2.5f
                                         , 0, 1);
                audioSource.Play();
            }
            _lastVelocity = _rigidbody.velocity;
        }
    }
}
