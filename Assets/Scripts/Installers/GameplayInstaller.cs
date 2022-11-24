using Factories;
using Managers;
using Monobehaviors.Entities;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameWorldStateManager>().AsSingle().NonLazy();
            Container.Bind<GameObjectFactory>().AsSingle().NonLazy();
            Container.Bind<EntityPoolManager<EnemyShip>>().AsCached().NonLazy();
            Container.Bind<EntityPoolManager<Bullet>>().AsCached().NonLazy();
        }
    }
}
