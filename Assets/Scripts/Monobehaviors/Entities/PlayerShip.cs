using Managers;
using UnityEngine;
using Zenject;

namespace Monobehaviors.Entities
{
    public class PlayerShip : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        
        [Inject] private GameWorldStateManager _gameWorldStateManager;

        private Camera _gameplayCamera;

        public void Initialize()
        {
            _gameplayCamera = _gameWorldStateManager.GamePlayCamera;
        }
        
        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var mouseWorldPos = _gameplayCamera.ScreenToWorldPoint(Input.mousePosition);
                var t = transform.position;
                float targetX = t.x;

                if (mouseWorldPos.x > t.x)
                {
                    targetX += _speed * Time.deltaTime;
                }
                else if (mouseWorldPos.x < t.x)
                {
                    targetX -= _speed * Time.deltaTime;
                }

                transform.position = new Vector3(targetX, t.y, t.z);
            }
        }
    }
}
