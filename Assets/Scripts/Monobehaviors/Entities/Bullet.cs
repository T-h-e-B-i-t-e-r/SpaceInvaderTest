using System;
using Managers;
using UnityEngine;
using Zenject;

namespace Monobehaviors.Entities
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 4f;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        private float _topLimit;
        
        // injections
        private GameWorldStateManager _gameWorldStateManager;
        private EntityPoolManager<Bullet> _bulletPoolManager;
        
        [Inject]
        private void InjectDependencies(
            GameWorldStateManager gameWorldStateManager,
            EntityPoolManager<Bullet> bulletPoolManager
        )
        {
            _gameWorldStateManager = gameWorldStateManager;
            _bulletPoolManager = bulletPoolManager;
        }

        private void Start()
        {
            _topLimit = _gameWorldStateManager.GameWorldState.TopOfScreen;
        }

        void FixedUpdate()
        {
            Vector3 position = _rigidbody2D.position;
            float targetY = position.y;
            
            if (targetY < _topLimit)
            {
                targetY += _speed * Time.deltaTime;
                _rigidbody2D.MovePosition(new Vector2(position.x, targetY));
            }
            else
            {
                _bulletPoolManager.ReturnObjectToPool(this);
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            _bulletPoolManager.ReturnObjectToPool(this);
        }
    }
}
