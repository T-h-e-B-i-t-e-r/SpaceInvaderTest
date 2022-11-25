using Data;
using Factories;
using Managers;
using Monobehaviors.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Monobehaviors
{
    public class GameplayEntryScript : MonoBehaviour
    {
        // local
        [SerializeField] private Camera _gameplayCamera;
        [SerializeField] private TextMeshProUGUI _enemyCountLabel;
        [SerializeField] private TextMeshProUGUI _scoreLabel;
        [SerializeField] private Transform _enemyPoolParent;
        [SerializeField] private int _enemyCount;
        [SerializeField] private Transform _bulletPoolParent;
        [SerializeField] private int _bulletCount;
        
        // injections
        private GameWorldStateManager _gameWorldStateManager;
        private GameObjectFactory _gameObjectFactory;
        private EntityPoolManager<EnemyShip> _enemyPoolManager;
        private EntityPoolManager<Bullet> _bulletPoolManager;
        private EnemyShipSpawner _enemyShipSpawner;
        
        [Inject]
        private void InjectDependencies(
            GameWorldStateManager gameWorldStateManager,
            GameObjectFactory gameObjectFactory,
            EntityPoolManager<EnemyShip> enemyPoolManager,
            EntityPoolManager<Bullet> bulletPoolManager,
            EnemyShipSpawner enemyShipSpawner
            )
        {
            _gameWorldStateManager = gameWorldStateManager;
            _gameObjectFactory = gameObjectFactory;
            _enemyPoolManager = enemyPoolManager;
            _bulletPoolManager = bulletPoolManager;
            _enemyShipSpawner = enemyShipSpawner;
        }
        
        private void Start()
        {
            InitializeGameWorld();
        }

        private void InitializeGameWorld()
        {
            UpdateEnemyLabel(_enemyCount);
            UpdateScoreLabel(0);
            _gameWorldStateManager.Initialize(_gameplayCamera, _enemyCount, UpdateEnemyLabel, UpdateScoreLabel);
            CreatePlayer();
            _enemyPoolManager.Initialize(GameConstants.EnemyPrefab, _enemyCount, _enemyPoolParent, InitializeEnemySpawner);
            _bulletPoolManager.Initialize(GameConstants.BulletPrefab, _bulletCount, _bulletPoolParent);
        }

        private void CreatePlayer()
        {
            Addressables.LoadAssetAsync<GameObject>(GameConstants.PlayerPrefab).Completed += OnPlayerObjectLoaded; // TODO: probably wrap this in its own loader class
        }

        private void OnPlayerObjectLoaded(AsyncOperationHandle<GameObject> asyncOperationHandle)
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Player Loaded successfully");
                var loadedPlayerPrefab = asyncOperationHandle.Result;
                var playerGo = _gameObjectFactory.CreateGameObject(null, loadedPlayerPrefab);
                playerGo.transform.position = _gameplayCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.05f, 1)); // bottom 5% of screen, in the middle
            }
        }

        private void InitializeEnemySpawner()
        {
            _enemyShipSpawner.Initialize();
        }

        private void UpdateEnemyLabel(int enemyCount)
        {
            _enemyCountLabel.text = string.Format(GameConstants.EnemyCountLabel, enemyCount);
        }

        private void UpdateScoreLabel(int score)
        {
            _scoreLabel.text = string.Format(GameConstants.ScoreLabel, score);
        }
    }
}
