using UnityEngine;

namespace Units.Configs
{
    public class UnitsPoolConfiguration : MonoBehaviour
    {
        [SerializeField] private Transform _pooledParent;
        [SerializeField] private Transform _activeParent; 
        [SerializeField] private MonoBehaviour _prefab;
        [SerializeField] private int _amount;
        [SerializeField] private UnitConfiguration _unitConfiguration;

        public Transform PooledParent => _pooledParent;
        public Transform ActiveParent => _activeParent;
        public MonoBehaviour Prefab => _prefab;
        public int Amount => _amount;
        public UnitConfiguration UnitConfiguration => _unitConfiguration;
    }
}
