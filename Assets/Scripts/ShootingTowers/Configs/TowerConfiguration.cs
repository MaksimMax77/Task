using ShootingTowers.Projectiles;
using UnityEngine;

namespace ShootingTowers.Configs
{
    public class TowerConfiguration : MonoBehaviour
    {
        [SerializeField] private TowerGameObject _towerGameObject;
        [SerializeField] private float _shootInterval;
        [SerializeField] private float _range;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private float _projectileSpeed;

        public TowerGameObject TowerGameObject => _towerGameObject;
        public float ShootInterval => _shootInterval;
        public float Range => _range;
        public Projectile ProjectilePrefab => _projectilePrefab;
        public float ProjectileSpeed => _projectileSpeed;
    }
}
