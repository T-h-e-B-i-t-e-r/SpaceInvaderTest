using System.Collections.Generic;
using Factories;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Managers
{
    public class EntityPoolManager<T> where T : MonoBehaviour
    {
        [Inject] private GameObjectFactory _gameObjectFactory;
        
        private GameObject _loadedGameObject;
        private Transform _parent;
        private int _amountToCreate;
        private List<T> _availablePooledObjects = new List<T>();
        private UnityAction _onCompleteCallback;
        
        public void Initialize(string addressableKey, int count, Transform parent, UnityAction onCompleteCallback = null)
        {
            _parent = parent;
            _amountToCreate = count;
            _onCompleteCallback = onCompleteCallback;
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
                    var go = _gameObjectFactory.CreateGameObject(_parent, _loadedGameObject);
                    _availablePooledObjects.Add(go.GetComponent<T>());
                }
                
                _onCompleteCallback?.Invoke();
            }
        }

        public T PeekObjectFromPool()
        {
            if (_availablePooledObjects.Count > 0)
            {
                var obj = _availablePooledObjects[0];
                return obj;
            }

            return null;
        }
        
        public T GetObjectFromPool(Transform newParent)
        {
            if (_availablePooledObjects.Count > 0)
            {
                var obj = _availablePooledObjects[0];
                obj.transform.SetParent(newParent);
                _availablePooledObjects.Remove(obj);
                return obj;
            }

            return null;
        }

        public void ReturnObjectToPool(T objectToPool)
        {
            var t = objectToPool.transform;
            t.SetParent(_parent);
            t.localPosition = Vector3.zero;
            _availablePooledObjects.Add(objectToPool);
        }
    }
}
