using ShootingTowers;
using ShootingTowers.Configs;
using Units;
using Units.Configs;
using UnityEngine;
using Update;

namespace Installer
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private UnitsManagerSettings _unitsManagerSettings;
        [SerializeField] private GlobalUpdate _globalUpdate;
        [SerializeField] private UnitsPoolConfiguration _unitsPoolConfiguration;
        [SerializeField] private ShootingConfiguration _magicTowerShootingConfiguration;
        [SerializeField] private Tower _tower;
        private UnitsPool _unitsPool;
        private UnitsManager _unitsManager;
        private ShootingControl _magicTowerShootingControl;
        private MagicTower _magicTower;

        private void Awake()
        {
            _unitsPool = new UnitsPool(_unitsPoolConfiguration);
            _unitsManager = new UnitsManager(_unitsPool, _unitsManagerSettings);
            _magicTowerShootingControl = new ShootingControl(_unitsManager, _magicTowerShootingConfiguration);
            _magicTower = new MagicTower(_magicTowerShootingControl);
            _globalUpdate.AddUpdatableObject(_unitsManager);
            _globalUpdate.AddUpdatableObject(_magicTower);
            _tower.Init(_unitsManager);
        }
    }
}
