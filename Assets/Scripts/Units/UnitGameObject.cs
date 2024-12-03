using ShootingTowers.Projectiles;
using UnityEngine;

namespace Units
{
    public class UnitGameObject : MonoBehaviour
    {
        [SerializeField] private ProjectileHitHandler _projectileHitHandler;
        public ProjectileHitHandler ProjectileHitHandler => _projectileHitHandler;
    }
}
