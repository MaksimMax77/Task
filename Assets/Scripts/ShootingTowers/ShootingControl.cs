using ShootingTowers.Configs;
using Units;
using UnityEngine;

namespace ShootingTowers
{
    public class ShootingControl  
    {
        private ShootingGameObject _shootingGameObject;
        private float _shootInterval;
        private float _range;
        private Projectile _projectilePrefab;
        private UnitsManager _unitsManager;
        private float _lastShotTime = -0.5f;

        public Transform ShootPoint => _shootingGameObject.ShootPoint;

        public ShootingControl(UnitsManager unitsManager, ShootingConfiguration shootingConfiguration) 
        {
            _unitsManager = unitsManager;
            _shootingGameObject = shootingConfiguration.ShootingGameObject;
            _shootInterval = shootingConfiguration.ShootInterval;
            _range = shootingConfiguration.Range;
            _projectilePrefab = shootingConfiguration.ProjectilePrefab;
        }
        
        public bool TryGetUnitByRangeDistance(out Unit unit)
        {
            unit = null;

            for (int i = 0, len = _unitsManager.Units.Count; i < len; ++i)
            {
                if (Vector3.Distance(_shootingGameObject.transform.position,
                        _unitsManager.Units[i].UnitObj.transform.position) > _range)
                {
                    continue;
                }

                unit = _unitsManager.Units[i];
                return true;
            }

            return false;
        }

        public bool ShootIntervalIsEnd()
        {
            if (_lastShotTime + _shootInterval > Time.time)
            {
                return false;
            }

            _lastShotTime = Time.time;
            return true;
        }
        
        public Projectile CreateProjectile()
        {
            var projectile = Object.Instantiate(_projectilePrefab);
            projectile.transform.position = _shootingGameObject.ShootPoint.position;
            return projectile;
        }
    }
}