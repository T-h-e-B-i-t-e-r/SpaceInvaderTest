using Factories;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemyShipFactory>().AsSingle().NonLazy();
        }
    }
}
