using Dispose;
using ShootingTowers;
using ShootingTowers.Configs;
using Units;
using Units.Configs;
using UnityEngine;
using Update;

namespace Core
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private UnitsManagerSettings _unitsManagerSettings;
        [SerializeField] private UnitsPoolConfiguration _unitsPoolConfiguration;
        [SerializeField] private TowerConfiguration _magicTowerConfiguration;
        [SerializeField] private CannonTowerConfiguration _cannonTowerConfiguration; 
        [SerializeField] private GlobalUpdate _globalUpdate;
        [SerializeField] private Disposer _disposer;
        private UnitsPool _unitsPool;
        private UnitsManager _unitsManager;
        private MagicTower _magicTower;
        private CannonTower _cannonTower;

        private void Awake()
        {
            _unitsPool = new UnitsPool(_unitsPoolConfiguration);
            _unitsManager = new UnitsManager(_unitsPool, _unitsManagerSettings);
            _magicTower = new MagicTower(_unitsManager, _magicTowerConfiguration);
            _cannonTower = new CannonTower(_unitsManager, _cannonTowerConfiguration);
            _globalUpdate.AddUpdatableObject(_unitsManager);
            _globalUpdate.AddUpdatableObject(_magicTower);
            _globalUpdate.AddUpdatableObject(_cannonTower);
            _disposer.Add(_unitsManager);
        }
    }
}
