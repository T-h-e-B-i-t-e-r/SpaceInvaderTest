using Factories;
using Managers;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameWorldStateManager>().AsSingle().NonLazy();
            Container.Bind<GameObjectFactory>().AsSingle().NonLazy();
            Container.Bind<EntityPoolManager>().WithId("Enemy").AsCached().NonLazy();
            Container.Bind<EntityPoolManager>().WithId("Bullet").AsCached().NonLazy();
        }
    }
}
