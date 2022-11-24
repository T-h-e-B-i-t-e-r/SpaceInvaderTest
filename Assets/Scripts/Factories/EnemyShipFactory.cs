using Monobehaviors.Entities;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class EnemyShipFactory
    {
        [Inject] private DiContainer _diContainer;

        public EnemyShip CreateEnemy(Vector3 position)
        {
            //_diContainer.InstantiatePrefab()
            return null;
        }
    }
}
