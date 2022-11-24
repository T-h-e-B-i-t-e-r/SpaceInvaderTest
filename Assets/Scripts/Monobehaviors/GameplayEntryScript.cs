using Data;
using Factories;
using Managers;
using TMPro;
using UnityEngine;
using Zenject;

namespace Monobehaviors
{
    public class GameplayEntryScript : MonoBehaviour
    {
        [SerializeField] private Camera _gameplayCamera;
        [SerializeField] private TextMeshProUGUI _enemyCountLabel;
        [SerializeField] private TextMeshProUGUI _scoreLabel;
        [SerializeField] private Transform _enemyPoolParent;
        [SerializeField] private int _enemyCount = 50;
        [SerializeField] private Transform _bulletPoolParent;
        [SerializeField] private int _bulletCount = 20;
        
        // injections
        private GameWorldStateManager _gameWorldStateManager;
        private EntityPoolManager _enemyPoolManager;
        private EntityPoolManager _bulletPoolManager;
        
        [Inject]
        private void InjectDependencies(
            GameWorldStateManager gameWorldStateManager,
            [Inject(Id = "Enemy")]EntityPoolManager enemyPoolManager,
            [Inject(Id = "Bullet")]EntityPoolManager bulletPoolManager
            )
        {
            _gameWorldStateManager = gameWorldStateManager;
            _enemyPoolManager = enemyPoolManager;
            _bulletPoolManager = bulletPoolManager;
        }
        
        private void Start()
        {
            InitializeGameWorld();
        }

        private void InitializeGameWorld()
        {
            _gameWorldStateManager.Initialize(_gameplayCamera);
            _enemyPoolManager.Initialize(AddressablesConstants.EnemyPrefab, _enemyCount, _enemyPoolParent);
            _bulletPoolManager.Initialize(AddressablesConstants.BulletPrefab, _bulletCount, _bulletPoolParent);
        } 
    }
}
