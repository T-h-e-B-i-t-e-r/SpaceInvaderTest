using Monobehaviors.Entities;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class EnemyShipSpawner 
    {
        // local
        private Vector3 _spawnLocationCenter;
        private Vector2 _enemyShipOffset;
        private Vector2 _enemyShipSize;
        private int _enemyCountPerRow = 5;
        
        // injections
        private GameWorldStateManager _gameWorldStateManager;
        private EntityPoolManager<EnemyShip> _enemyPoolManager;
        
        [Inject]
        private void InjectDependencies(
            GameWorldStateManager gameWorldStateManager,
            EntityPoolManager<EnemyShip> enemyPoolManager
        )
        {
            _gameWorldStateManager = gameWorldStateManager;
            _enemyPoolManager = enemyPoolManager;
        }

        public void Initialize()
        {
            var gameplayCamera = _gameWorldStateManager.GamePlayCamera;
            _spawnLocationCenter = gameplayCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            var enemyShip = _enemyPoolManager.PeekObjectFromPool();
            var size = enemyShip.GetComponent<SpriteRenderer>().sprite.bounds.size;
            var scale = enemyShip.transform.localScale;
            _enemyShipSize = new Vector2(size.x * scale.x, size.y * scale.y);
            _enemyShipOffset = new Vector2(_enemyShipSize.x / 3, _enemyShipSize.y * 1.5f);

            var rowsToCreate = _gameWorldStateManager.GameWorldState.EnemyCount / _enemyCountPerRow; 
            for (int i = 0; i < rowsToCreate; i++)
            {
                CreateEnemyLine();
            }
        }

        private void CreateEnemyLine()
        {
            for (int i = 0; i < _enemyCountPerRow; i++)
            {
                var posX = _spawnLocationCenter.x + (-_enemyCountPerRow / 2 + i) * (_enemyShipSize.x + _enemyShipOffset.x);
                var enemyShip = _enemyPoolManager.GetObjectFromPool(null);

                if (enemyShip != null)
                {
                    var t = enemyShip.transform;
                    t.position = new Vector3(posX, _spawnLocationCenter.y, t.position.z);
                }
            }

            _spawnLocationCenter = new Vector3(_spawnLocationCenter.x, _spawnLocationCenter.y + _enemyShipOffset.y, _spawnLocationCenter.z);
        }
    }
}
