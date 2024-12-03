using UnityEngine;

namespace ShootingTowers.Configs
{
    public class CannonTowerConfiguration : TowerConfiguration
    {
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private Transform _rotateObj;
        [SerializeField] private bool _isParabolicTrajectory;

        public float RotateSpeed => _rotateSpeed;
        public Transform RotateObj => _rotateObj;
        public bool IsParabolicTrajectory => _isParabolicTrajectory;
    }
}