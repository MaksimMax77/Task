using System;
using Units.Configs;
using UnityEngine;

namespace Units
{
    public class Unit
    {
        public event Action<Unit> Death;
        private float _speed;
        private float _maxHp;
        private float _currentHp;
        private float _reachDistance;
        private Transform _destination;
        private Vector3 _lastSpeed;
        private UnitGameObject _unitObj;
        
        public Vector3 LastSpeed => _lastSpeed;
        public UnitGameObject UnitObj => _unitObj;

        public Unit(UnitConfiguration unitConfiguration, UnitGameObject unitObj)
        {
            _speed = unitConfiguration.speed;
            _maxHp = unitConfiguration.maxHp;
            _reachDistance = unitConfiguration.reachDistance;
            _currentHp = _maxHp;
            _unitObj = unitObj;
            _unitObj.ProjectileHitHandler.Hit += GetDamage;
        }

        public void Dispose()
        {
            _unitObj.ProjectileHitHandler.Hit -= GetDamage;
        }

        public void SetPosition(Vector3 position)
        {
            _unitObj.transform.position = position;
        }
        
        public void SetDestination(Transform destination)
        {
            _destination = destination;
        }

        private void GetDamage(float damage)
        {
            _currentHp -= damage;

            if (_currentHp <= 0)
            {
                Death?.Invoke(this);
            }
        }

        public void Update()
        {
            if (_destination == null)
            {
                return;
            }

            var direction = _destination.position - _unitObj.transform.position;
            
            if (direction.magnitude <= _reachDistance)
            {
                Death?.Invoke(this);
                return;
            }

            _lastSpeed = direction.normalized * _speed;
            var transform = _unitObj.transform;
            transform.position += _lastSpeed * Time.deltaTime;
            transform.forward = _lastSpeed;
        }
    }
}
