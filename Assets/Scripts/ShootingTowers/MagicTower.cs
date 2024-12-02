using Update;

namespace ShootingTowers
{
    public class MagicTower : IUpdatable
    {
        private ShootingControl _shootingControl;

        public MagicTower(ShootingControl shootingControl)
        {
            _shootingControl = shootingControl;
        }

        public void Update()
        {
            if (!_shootingControl.TryGetUnitByRangeDistance(out var unit)
                || !_shootingControl.ShootIntervalIsEnd())
            {
                return;
            }

            var projectile = _shootingControl.CreateProjectile();
            projectile.SetVelocity((unit.UnitObj.transform.position - _shootingControl.ShootPoint.position).normalized *
                                   5); //todo исправить хардкод
        }


        /*void Update()
        {
            if (m_projectilePrefab == null)
                return;

            /*foreach (var monster in FindObjectsOfType<Monster>())
        {
            if (Vector3.Distance(transform.position, monster.transform.position) > m_range)
                continue;

            if (m_lastShotTime + m_shootInterval > Time.time)
                continue;

            // shot
            var projectile =
                Instantiate(m_projectilePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity) as
                    GameObject;
            var projectileBeh = projectile.GetComponent<GuidedProjectile>();
            projectileBeh.m_target = monster.gameObject;

            m_lastShotTime = Time.time;
        }#1#
        }*/
    }
}