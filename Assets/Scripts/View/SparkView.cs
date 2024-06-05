using UnityEngine;

namespace View
{
    public class SparkView : MonoBehaviour
    {
        public float sparkImpactIntensity;
        public float sparkSlipIntensity;
        private ParticleSystem _particlesImpact;
        private Transform _particlesImpactTransform;
        private Vector3 _forward;
        private bool _sparkSlipState;

        private void Start()
        {
            var particlesImpactObject = transform.Find("Particle System impact");
            _forward = GetComponent<Transform>().forward;
            _particlesImpact = particlesImpactObject.GetComponent<ParticleSystem>();
            _particlesImpactTransform = particlesImpactObject.GetComponent<Transform>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var direction = other.contacts[0].normal
                            - new Vector2(_forward.x, _forward.y);
            var angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + 90;
            _particlesImpactTransform.rotation = Quaternion.Euler(angle, 90, -90);
            _particlesImpact.Emit(Mathf.RoundToInt(sparkImpactIntensity * other.relativeVelocity.magnitude));
        }
    
        private void OnCollisionStay2D(Collision2D other)
        {
            var direction = other.contacts[0].normal
                            - new Vector2(_forward.x, _forward.y);
            var angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + 90;
            _particlesImpactTransform.rotation = Quaternion.Euler(angle, 90, -90);
            _particlesImpact.Emit(Mathf.RoundToInt(sparkSlipIntensity * other.relativeVelocity.magnitude));
        }

    }
}
