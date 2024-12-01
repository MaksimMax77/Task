using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class UnitsManager : MonoBehaviour
    {
        [SerializeField] SpawnerSettings _spawnerSettings;
        [SerializeField] private float _interval = 3;
        [SerializeField] private GameObject _moveTarget;
        [SerializeField] private Transform _pooledParent; 
        [SerializeField] private Transform _activeParent; 
        private float _lastSpawn = -1;
        private List<Monster> _pool = new List<Monster>();
        private List<Monster> _activeMonsters =  new List<Monster>();

        public List<Monster> ActiveMonsters => _activeMonsters;

        private void Awake()
        {
            for (int i = 0; i < _spawnerSettings.amount; i++)
            {
                var monster = Instantiate(_spawnerSettings.monsterPrefab, transform.position, Quaternion.identity);
                monster.transform.SetParent(_pooledParent);
                monster.SetUnitsManager(this);
                _pool.Add(monster);
            }
        }

        private void Update()
        {
            if (Time.time > _lastSpawn + _interval)
            {
                Spawn();
                _lastSpawn = Time.time;
            }
        }

        public void UnitReturnToPool(Monster monster)
        {
            _activeMonsters.Remove(monster);
            _pool.Add(monster);
            monster.transform.SetParent(_pooledParent);
        }

        private void Spawn()
        {
            if (!TryGetAndRemoveUnitFromPool(out var monster))
            {
                return;
            }

            monster.transform.position = transform.position;
            monster.SetDestination(_moveTarget.transform);
        }

        private bool TryGetAndRemoveUnitFromPool(out Monster monster)
        {
            monster = null;
            if (_pool.Count == 0)
            {
                return false;
            }

            monster = _pool[0];
            _pool.Remove(monster);
            monster.transform.SetParent(_activeParent);
            _activeMonsters.Add(monster);
            return true;
        }
    }

    [Serializable]
    public struct SpawnerSettings
    {
        public int amount;
        public Monster monsterPrefab;
    }
}