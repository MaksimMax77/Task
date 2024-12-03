using System.Collections.Generic;
using ShootingTowers.Configs;
using ShootingTowers.Projectiles;
using Units;
using UnityEngine;
using Update;

namespace ShootingTowers
{
    public class MagicTower : Tower<TowerConfiguration>, IUpdatable
    {
        private List<Projectile> _projectiles = new List<Projectile>();
        private Unit _unit;

        public MagicTower(UnitsManager unitsManager, TowerConfiguration towerConfiguration)
            : base(unitsManager, towerConfiguration)
        {
        }

        public void Update()
        {
            if (!IsEnabled())
            {
                return;
            }
            
            if (_unit == null)
            {
                if (!TryGetUnitByRangeDistance(out var unit))
                {
                    return;
                }

                _unit = unit;
                _unit.Death += SetUnitNull;
            }

            MoveProjectiles(_unit.UnitObj);
            
            if (!ShootIntervalIsEnd())
            {
                return;
            }
            
            var projectile = Object.Instantiate(_projectilePrefab, _towerGameObject.ShootPoint.position,
                Quaternion.identity);
            _projectiles.Add(projectile);
        }

        private void SetUnitNull(Unit unit)
        {
            _unit.Death -= SetUnitNull;
            _unit = null;
        }
        
        private void MoveProjectiles(UnitGameObject unitGameObject)
        {
            for (var i = _projectiles.Count - 1; i >= 0; i--)
            {
                var projectile = _projectiles[i];

                if (projectile == null || unitGameObject == null)
                {
                    return;
                }
                
                projectile.SetVelocity((unitGameObject.transform.position 
                                        - projectile.transform.position).normalized * _projectileSpeed);
            }
        }
    }
}