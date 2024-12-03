using UnityEngine;

namespace ShootingTowers
{
    public class TowerGameObject : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        public Transform ShootPoint => _shootPoint;
    }
}