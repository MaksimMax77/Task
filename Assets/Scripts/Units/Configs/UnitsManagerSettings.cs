using UnityEngine;

namespace Units.Configs
{
    public class UnitsManagerSettings : MonoBehaviour
    {
        [SerializeField] private float _interval = 3;
        [SerializeField] private Transform _moveTarget;
        [SerializeField] private Transform _unitStartPoint;

        public float Interval => _interval;
        public Transform MoveTarget => _moveTarget;
        public Transform UnitStartPoint => _unitStartPoint;
    }
}