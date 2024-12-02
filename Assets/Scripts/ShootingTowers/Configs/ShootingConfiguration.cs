using UnityEngine;

namespace ShootingTowers.Configs
{
    public class ShootingConfiguration : MonoBehaviour
    {
        [SerializeField] private ShootingGameObject shootingGameObject;
        [SerializeField] private float _shootInterval;
        [SerializeField] private float _range;
        [SerializeField] private Projectile _projectilePrefab;

        public ShootingGameObject ShootingGameObject => shootingGameObject;
        public float ShootInterval => _shootInterval;
        public float Range => _range;
        public Projectile ProjectilePrefab => _projectilePrefab;
    }
}
