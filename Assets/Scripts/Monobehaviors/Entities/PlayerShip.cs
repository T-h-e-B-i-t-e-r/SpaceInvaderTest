using Managers;
using UnityEngine;
using Zenject;

namespace Monobehaviors.Entities
{
    public class PlayerShip : MonoBehaviour
    {
        // local
        [SerializeField] private float _speed;
        [SerializeField] private float _firingRate;
        private Camera _gameplayCamera;
        private float _firingCounter;
        private Vector2 _horizontalLimits;
        
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

        public void Initialize()
        {
            _gameplayCamera = _gameWorldStateManager.GamePlayCamera;
            _firingCounter = 0;
            var shipHalfWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2 * transform.localScale.x;
            _horizontalLimits = new Vector2(_gameWorldStateManager.GameWorldState.HorizontalScreenLimits.x + shipHalfWidth,
                _gameWorldStateManager.GameWorldState.HorizontalScreenLimits.y - shipHalfWidth);
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

                targetX = Mathf.Clamp(targetX, _horizontalLimits.x, _horizontalLimits.y);
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
            var bullet = _bulletPoolManager.GetObjectFromPool(null);

            if (bullet != null)
            {
                bullet.transform.position = transform.position;
            }
        }
    }
}
