using System;
using Managers;
using UnityEngine;
using Zenject;

namespace Monobehaviors.Entities
{
    public class EnemyShip : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
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
        
        void Update()
        {
        
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            _enemyPoolManager.ReturnObjectToPool(this);
            _gameWorldStateManager.RegisterEnemyDestroyed();
        }
    }
}
