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
        [SerializeField] private int _enemyCount = 50;
        [SerializeField] private Transform _bulletPoolParent;
        [SerializeField] private int _bulletCount = 20;
        
        // injections
        private GameWorldStateManager _gameWorldStateManager;
        private GameObjectFactory _gameObjectFactory;
        private EntityPoolManager _enemyPoolManager;
        private EntityPoolManager _bulletPoolManager;
        
        [Inject]
        private void InjectDependencies(
            GameWorldStateManager gameWorldStateManager,
            GameObjectFactory gameObjectFactory,
            [Inject(Id = "Enemy")]EntityPoolManager enemyPoolManager,
            [Inject(Id = "Bullet")]EntityPoolManager bulletPoolManager
            )
        {
            _gameWorldStateManager = gameWorldStateManager;
            _gameObjectFactory = gameObjectFactory;
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
            CreatePlayer();
            _enemyPoolManager.Initialize(AddressablesConstants.EnemyPrefab, _enemyCount, _enemyPoolParent);
            _bulletPoolManager.Initialize(AddressablesConstants.BulletPrefab, _bulletCount, _bulletPoolParent);
        }

        private void CreatePlayer()
        {
            Addressables.LoadAssetAsync<GameObject>(AddressablesConstants.PlayerPrefab).Completed += OnPlayerObjectLoaded; // TODO: probably wrap this in its own loader class
        }

        private void OnPlayerObjectLoaded(AsyncOperationHandle<GameObject> asyncOperationHandle)
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Player Loaded successfully");
                var loadedPlayerPrefab = asyncOperationHandle.Result;
                var playerGo = _gameObjectFactory.CreateGameObject(null, loadedPlayerPrefab);
                playerGo.transform.position = _gameplayCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.05f, 1)); // bottom 5% of screen, in the middle
                playerGo.GetComponent<PlayerShip>().Initialize();
            }
        }
    }
}
