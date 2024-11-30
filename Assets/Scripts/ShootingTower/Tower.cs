using Unit;
using UnityEngine;

namespace ShootingTower
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float _shootInterval = 0.5f;
        [SerializeField] private float _range = 4f;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private float _projectileSpeed = 5.0f;
        private float _lastShotTime = -0.5f;

        private void Update()
        {
            if (_projectilePrefab == null || _shootPoint == null)
            {
                return;
            }
            
            foreach (var monster in FindObjectsOfType<Monster>())
            {
                if (Vector3.Distance(transform.position, monster.transform.position) > _range)
                {
                    continue;
                }

                if (_lastShotTime + _shootInterval > Time.time)
                {
                    continue;
                }

                Attack(monster);
                _lastShotTime = Time.time;
            }
        }

        private void Attack(Monster monster)
        {
            var projectile = Instantiate(_projectilePrefab);
            projectile.transform.position = _shootPoint.position;

            var hitPoint = GetHitPoint(monster.transform.position,
                monster.LastSpeed, _shootPoint.position, _projectileSpeed);
            var aim = hitPoint - _shootPoint.position;
            var projectileVelocity = aim.normalized * _projectileSpeed;
            projectile.SetVelocity(projectileVelocity);
        }

        private Vector3 GetHitPoint(Vector3 targetPosition, Vector3 targetSpeed, Vector3 attackerPosition,
            float bulletSpeed)
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

            var time = Mathf.Max(t1, t2);

            var ret = targetPosition + targetSpeed * time;
            return ret;
        }
    }
}