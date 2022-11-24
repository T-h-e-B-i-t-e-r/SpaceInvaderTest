using Monobehaviors.Entities;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class GameObjectFactory
    {
        [Inject] private DiContainer _diContainer;

        public GameObject CreateGameObject(Transform parent, GameObject loadedGameObject)
        {
            var go = _diContainer.InstantiatePrefab(loadedGameObject, parent);
            return go;
        }
    }
}
