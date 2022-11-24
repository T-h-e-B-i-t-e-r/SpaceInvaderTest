using Managers;
using UnityEngine;
using Zenject;

namespace Monobehaviors.Entities
{
    public class PlayerShip : MonoBehaviour
    {
        // local
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _firingRate = 1f;
        private Camera _gameplayCamera;
        private float _firingCounter;
        
        // injections
        private GameWorldStateManager _gameWorldStateManager;
        private EntityPoolManager _bulletPoolManager;
        
        [Inject]
        private void InjectDependencies(
            GameWorldStateManager gameWorldStateManager,
            [Inject(Id = "Bullet")]EntityPoolManager bulletPoolManager
        )
        {
            _gameWorldStateManager = gameWorldStateManager;
            _bulletPoolManager = bulletPoolManager;
        }

        public void Initialize()
        {
            _gameplayCamera = _gameWorldStateManager.GamePlayCamera;
            _firingCounter = 0;
        }
        
        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var mouseWorldPos = _gameplayCamera.ScreenToWorldPoint(Input.mousePosition);
                var t = transform.position;
                float targetX = t.x;

                if (mouseWorldPos.x > targetX)
                {
                    targetX += _speed * Time.deltaTime;
                }
                else if (mouseWorldPos.x < targetX)
                {
                    targetX -= _speed * Time.deltaTime;
                }

                transform.position = new Vector3(targetX, t.y, t.z);
            }

            _firingCounter += Time.deltaTime;

            if (_firingCounter > _firingRate)
            {
                _firingCounter = 0;
                FireBullet();
            }
        }

        private void FireBullet()
        {
            var bullet = _bulletPoolManager.GetGameObjectFromPool(null);
            bullet.transform.position = transform.position;
        }
    }
}
