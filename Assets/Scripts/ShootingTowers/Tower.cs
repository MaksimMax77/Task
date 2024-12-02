using Units;
using UnityEngine;

namespace ShootingTowers
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float _range = 4f;
        [SerializeField] private float _projectileSpeed = 5.0f;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _shootInterval = 0.5f;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _rotateObj;
        [SerializeField] private bool _useGravity;
        private UnitsManager _unitsManager;
        private Vector3 _hitPoint;
        private float _lastShotTime = -0.5f;

        public void Init(UnitsManager unitsManager)
        {
            _unitsManager = unitsManager;
        }

        private void Update()
        {
            if (!TryGetUnitByRangeDistance(out var unit))
            {
                return;
            }

            _hitPoint = GetHitPoint(unit.UnitObj.transform.position,
                unit.LastSpeed, _shootPoint.position, _projectileSpeed, out var time);

            var direction = (_hitPoint - _shootPoint.position).normalized * _projectileSpeed;

            var gravity = 0f;

            if (_useGravity)
            {
                gravity = -Physics.gravity.y * time / 2;
                direction.y = gravity;
            }

            Rotate(direction);

            if (_lastShotTime + _shootInterval > Time.time)
            {
                return;
            }

            _lastShotTime = Time.time;
            Shoot(direction, gravity, time);
        }

        private bool TryGetUnitByRangeDistance(out Unit unit)
        {
            unit = null;
            
            for (int i = 0, len = _unitsManager.Units.Count; i < len; ++i)
            {
                if (Vector3.Distance(transform.position, 
                        _unitsManager.Units[i].UnitObj.transform.position) > _range)
                {
                    continue;
                }

                unit = _unitsManager.Units[i];
                return true;
            }

            return false;
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
            var projectile = Instantiate(_projectilePrefab);
            projectile.transform.position = _shootPoint.position;
            projectile.SetUseGravity(_useGravity);

            if (_useGravity)
            {
                var deltaY = (_hitPoint.y - projectile.transform.position.y) / time;
                direction.y = gravity + deltaY;
            }

            projectile.SetVelocity(direction);
        }
    }
}
