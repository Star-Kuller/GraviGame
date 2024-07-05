using UnityEngine;

public class BgGridOffset : MonoBehaviour
{
    public GameObject target;
    public float coefficient;
    private Rigidbody2D _rigidbody;
    private Rigidbody2D _otherRigidBody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _otherRigidBody = target.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _otherRigidBody.velocity * coefficient;
    }
}
