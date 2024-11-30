using Unit;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Monster _monsterPrefab;
    [SerializeField] private float _interval = 3;
    [SerializeField] private GameObject _moveTarget;
    private float _lastSpawn = -1;

    private void Update()
    {
        if (Time.time > _lastSpawn + _interval)
        {
            var monster = Instantiate(_monsterPrefab, transform.position,Quaternion.identity);
            monster.SetDestination(_moveTarget.transform);
            _lastSpawn = Time.time;
        }
    }
}