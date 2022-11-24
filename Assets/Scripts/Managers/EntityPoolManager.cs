using Factories;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Managers
{
    public class EntityPoolManager
    {
        [Inject] private GameObjectFactory _gameObjectFactory;
        
        private GameObject _loadedGameObject;
        private Transform _parent;
        private int _amountToCreate;
        
        public void Initialize(string addressableKey, int count, Transform parent)
        {
            _parent = parent;
            _amountToCreate = count;
            Addressables.LoadAssetAsync<GameObject>(addressableKey).Completed += OnGameObjectLoaded; // TODO: cleanup addressable references
        }

        private void OnGameObjectLoaded(AsyncOperationHandle<GameObject> asyncOperationHandle)
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Prefab data Loaded successfully");
                _loadedGameObject = asyncOperationHandle.Result;

                for (int i = 0; i < _amountToCreate; i++)
                {
                    _gameObjectFactory.CreateGameObject(_parent, _loadedGameObject);
                }
            }
        }

        public GameObject GetGameObjectFromPool(Transform newParent)
        {
            if (_parent.childCount > 0)
            {
                var gameObjectToReturnTransform = _parent.GetChild(0);
                gameObjectToReturnTransform.SetParent(newParent);
                return gameObjectToReturnTransform.gameObject;
            }

            return null;
        }

        public void ReturnObjectToPool(GameObject gameObject)
        {
            gameObject.transform.SetParent(_parent);
            gameObject.transform.localPosition = Vector3.zero;
        }
    }
}
