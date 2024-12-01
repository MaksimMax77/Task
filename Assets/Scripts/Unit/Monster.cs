using UnityEngine;

namespace Unit
{
    public class Monster : MonoBehaviour
    {
        [SerializeField] private float _speed = 3.0f;
        [SerializeField] private Transform _destination;
        [SerializeField] private float _maxHp;
        [SerializeField] private float _reachDistance = 0.3f;
        private Vector3 _lastSpeed;
        private float _currentHp;
        private UnitsManager _unitsManager;
        
        public Vector3 LastSpeed => _lastSpeed;

        public void SetDestination(Transform destination)
        {
            _destination = destination;
        }

        public void SetUnitsManager(UnitsManager unitsManager)
        {
            _unitsManager = unitsManager;
        }

        public void GetDamage(float damage)
        {
            _currentHp -= damage;

            if (_currentHp <= 0)
            {
                _unitsManager.UnitReturnToPool(this);
            }
        }

        private void Awake()
        {
            _currentHp = _maxHp;
        }

        private void Update()
        {
            if (_destination == null)
            {
                return;
            }
            
            if (Vector3.Distance(transform.position, _destination.transform.position) <= _reachDistance)
            {
                _unitsManager.UnitReturnToPool(this);
                return;
            }

            var direction = _destination.position - transform.position;

            if (direction.magnitude > 0.5f)
            {
                _lastSpeed = direction.normalized * _speed;
                transform.position += _lastSpeed * Time.deltaTime;
                transform.forward = _lastSpeed;
            }
            else
            {
                _lastSpeed = new Vector3(0, 0, 0);
            }
        }
    }
}