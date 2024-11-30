using Unit;
using UnityEngine;

namespace ShootingTower
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private int _damage = 10;
        private Vector3 _velocity;

        public void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
        }
    
        private void Update()
        {
            transform.position += _velocity * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            var monster = other.gameObject.GetComponent<Monster>();
            if (monster == null)
            {
                return;
            }
            
            monster.GetDamage(_damage);
            Destroy(gameObject);
        }
    }
}
