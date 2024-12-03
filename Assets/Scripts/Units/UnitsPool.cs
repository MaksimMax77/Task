using System.Collections.Generic;
using Units.Configs;
using UnityEngine;

namespace Units
{
    public class UnitsPool 
    {
        private readonly Transform _pooledParent;
        private readonly Transform _activeParent;
        private readonly List<Unit> _pooledObjects = new List<Unit>();
        private readonly List<Unit> _activeObjects = new List<Unit>();

        public UnitsPool(UnitsPoolConfiguration unitsPoolConfiguration)
        {
            _pooledParent = unitsPoolConfiguration.PooledParent;
            _activeParent = unitsPoolConfiguration.ActiveParent;
            CreateAndPutInPool(unitsPoolConfiguration.Amount, unitsPoolConfiguration);
        }

        private void CreateAndPutInPool(int amount, UnitsPoolConfiguration configuration)
        {
            for (var i = 0; i < amount; ++i)
            {
                var unitGameObject = Object.Instantiate((UnitGameObject)configuration.Prefab,
                    _pooledParent, true);
                var unit = new Unit(configuration.UnitConfiguration, unitGameObject);
                _pooledObjects.Add(unit);
            }
        }
        
        public void ReturnToPool(Unit unit)
        {
            _activeObjects.Remove(unit);
            _pooledObjects.Add(unit);
            unit.UnitObj.transform.SetParent(_pooledParent);
        }

        public bool TryGetAndSetActive(out Unit unit)
        {
            unit = null;
            if (_pooledObjects.Count == 0)
            {
                return false;
            }

            unit = _pooledObjects[0];
            _pooledObjects.Remove(unit);
            unit.UnitObj.transform.SetParent(_activeParent);
            _activeObjects.Add(unit);
            return true;
        }
    }
}
