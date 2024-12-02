using System.Collections.Generic;
using Units.Configs;
using UnityEngine;
using Update;

namespace Units
{
    public class UnitsManager : IUpdatable
    {
        private float _interval;
        private Transform _moveTarget;
        private Transform _unitStartPoint;
        private UnitsPool _unitsPool;
        private List<Unit> _units = new List<Unit>();
        private float _lastSpawn = -1;

        public List<Unit> Units => _units;

        public UnitsManager(UnitsPool unitsPool, UnitsManagerSettings unitsManagerSettings)
        {
            _unitsPool = unitsPool;
            _interval = unitsManagerSettings.Interval;
            _moveTarget = unitsManagerSettings.MoveTarget;
            _unitStartPoint = unitsManagerSettings.UnitStartPoint;
        }

        public void Update()
        {
            if (Time.time > _lastSpawn + _interval)
            {
                Spawn();
                _lastSpawn = Time.time;
            }

            UpdateUnits();
        }

        private void Spawn()
        {
            if (!_unitsPool.TryGetAndSetActive(out var unit))
            {
                return;
            }

            unit.SetPosition(_unitStartPoint.position);
            unit.SetDestination(_moveTarget.transform);
            unit.Death += OnUnitDeath;
            unit.DestinationReached += OnUnitDestinationReached;
            _units.Add(unit);
        }

        private void OnUnitDeath(Unit unit)
        {
            unit.Death -= OnUnitDeath;
            _units.Remove(unit);
            _unitsPool.ReturnToPool(unit);
        }

        private void OnUnitDestinationReached(Unit unit)
        {
            unit.DestinationReached -= OnUnitDestinationReached;
            _units.Remove(unit);
            _unitsPool.ReturnToPool(unit);
        }

        private void UpdateUnits()
        {
            for (var i = _units.Count - 1; i >= 0; i--) 
            {
                _units[i].Update();
            }
        }
    }
}
