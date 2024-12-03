using System;
using ShootingTowers.Projectiles;
using UnityEngine;

namespace Units
{
    public class ProjectileHitHandler : MonoBehaviour
    {
        public event Action<float> Hit;

        private void OnTriggerEnter(Collider other)
        {
            var projectile = other.gameObject.GetComponent<Projectile>();
            if (projectile == null)
            {
                return;
            }

            Hit?.Invoke(projectile.Damage);
            Destroy(projectile.gameObject);
        }
    }
}
