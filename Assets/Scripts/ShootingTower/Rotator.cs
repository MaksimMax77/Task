using UnityEngine;

namespace ShootingTower
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float _speed;
 
        public void Rotate(Vector3 targetPosition)
        {
            var time = Time.deltaTime * _speed;
            var direction = targetPosition - transform.position;
            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, time);
        }
    }
}