using Data;
using Managers;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().AsSingle().NonLazy();
            Container.Bind<GameResultsData>().AsSingle().NonLazy();
        }
    }
}
