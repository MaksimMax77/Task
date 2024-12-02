using UnityEngine;

namespace ShootingTowers
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private int _damage = 10;
        [SerializeField] private Rigidbody _rigidbody;
        private Vector3 _velocity;

        public float Damage => _damage;

        public void SetVelocity(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
        }

        public void SetUseGravity(bool value)
        {
            _rigidbody.useGravity = value;
        }
    
        private void Update()
        {
            transform.forward = _rigidbody.velocity;
        }
    }
}
