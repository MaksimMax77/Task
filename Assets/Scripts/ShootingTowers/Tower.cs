using ShootingTowers.Configs;
using ShootingTowers.Projectiles;
using Units;
using UnityEngine;

namespace ShootingTowers
{
    public abstract class Tower<T> where T : TowerConfiguration
    {
        protected TowerGameObject _towerGameObject;
        protected Projectile _projectilePrefab;
        protected float _projectileSpeed;
        private float _shootInterval;
        private float _range;
        private UnitsManager _unitsManager;
        private float _lastShotTime = -0.5f;

        protected Tower(UnitsManager unitsManager, T towerConfiguration)
        {
            _unitsManager = unitsManager;
            _towerGameObject = towerConfiguration.TowerGameObject;
            _shootInterval = towerConfiguration.ShootInterval;
            _range = towerConfiguration.Range;
            _projectilePrefab = towerConfiguration.ProjectilePrefab;
            _projectileSpeed = towerConfiguration.ProjectileSpeed;
        }

        protected bool IsEnabled()
        {
            return _towerGameObject.gameObject.activeInHierarchy;
        }
        
        protected bool TryGetUnitByRangeDistance(out Unit unit)
        {
            unit = null;

            for (int i = 0, len = _unitsManager.Units.Count; i < len; ++i)
            {
                if (Vector3.Distance(_towerGameObject.transform.position,
                        _unitsManager.Units[i].UnitObj.transform.position) > _range)
                {
                    continue;
                }

                unit = _unitsManager.Units[i];
                return true;
            }

            return false;
        }

        protected bool ShootIntervalIsEnd()
        {
            if (_lastShotTime + _shootInterval > Time.time)
            {
                return false;
            }

            _lastShotTime = Time.time;
            return true;
        }
    }
}
