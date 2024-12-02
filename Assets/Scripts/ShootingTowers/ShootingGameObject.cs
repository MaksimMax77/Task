using UnityEngine;

namespace ShootingTowers
{
    public class ShootingGameObject : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        public Transform ShootPoint => _shootPoint;
    }
}
