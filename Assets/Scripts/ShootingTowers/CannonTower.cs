using ShootingTowers.Configs;
using Units;
using UnityEngine;
using Update;

namespace ShootingTowers
{
    public class CannonTower : Tower<CannonTowerConfiguration>, IUpdatable
    {
        private float _rotateSpeed;
        private Transform _rotateObj;
        private bool _isParabolicTrajectory; 
        private Vector3 _hitPoint;
        private float _projectileDestinationTime;

        public CannonTower(UnitsManager unitsManager, CannonTowerConfiguration towerConfiguration)
            : base(unitsManager, towerConfiguration)
        {
            _rotateSpeed = towerConfiguration.RotateSpeed;
            _rotateObj = towerConfiguration.RotateObj;
            _isParabolicTrajectory = towerConfiguration.IsParabolicTrajectory;
        } 

        public void Update()
        {
            if (!TryGetUnitByRangeDistance(out var unit) || !IsEnabled())
            {
                return;
            }

            _hitPoint = GetHitPoint(unit.UnitObj.transform.position,
                unit.LastSpeed, _towerGameObject.ShootPoint.position, _projectileSpeed, out _projectileDestinationTime);

            var direction = (_hitPoint - _towerGameObject.ShootPoint.position).normalized * _projectileSpeed;

            var gravity = 0f;
            if (_isParabolicTrajectory)
            {
                gravity = -Physics.gravity.y * _projectileDestinationTime / 2;
                direction.y = gravity;
            }
            
            Rotate(direction);

            if (!ShootIntervalIsEnd())
            {
                return;
            }

            Shoot(direction, gravity, _projectileDestinationTime);
        }

        private Vector3 GetHitPoint(Vector3 targetPosition, Vector3 targetSpeed, Vector3 attackerPosition,
            float bulletSpeed, out float time)
        {
            var q = targetPosition - attackerPosition;
            q.y = 0;
            targetSpeed.y = 0;

            var a = Vector3.Dot(targetSpeed, targetSpeed) - (bulletSpeed * bulletSpeed);
            var b = 2 * Vector3.Dot(targetSpeed, q);
            var c = Vector3.Dot(q, q);

            var D = Mathf.Sqrt((b * b) - 4 * a * c);

            var t1 = (-b + D) / (2 * a);
            var t2 = (-b - D) / (2 * a);

            time = Mathf.Max(t1, t2);

            var ret = targetPosition + targetSpeed * time;
            return ret;
        }

        private void Rotate(Vector3 direction)
        {
            var time = Time.deltaTime * _rotateSpeed;
            var targetRotation = Quaternion.LookRotation(direction);
            _rotateObj.rotation = Quaternion.Lerp(_rotateObj.rotation, targetRotation, time);
        }

        private void Shoot(Vector3 direction, float gravity, float time)
        {
            var projectile = Object.Instantiate(_projectilePrefab);
            projectile.transform.position = _towerGameObject.ShootPoint.position;
            projectile.SetUseGravity(_isParabolicTrajectory);

            if (_isParabolicTrajectory)
            {
                var deltaY = (_hitPoint.y - projectile.transform.position.y) / time;
                direction.y = gravity + deltaY;
            }

            projectile.SetVelocity(direction);
        }
    }
}