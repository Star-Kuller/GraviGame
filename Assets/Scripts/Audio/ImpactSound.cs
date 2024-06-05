using UnityEngine;

namespace Audio
{
    public class ImpactSound : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter2D(Collision2D _)
        {
            if (_audioSource != null)
            {
                _audioSource.Play();
            }
        }
    }
}
