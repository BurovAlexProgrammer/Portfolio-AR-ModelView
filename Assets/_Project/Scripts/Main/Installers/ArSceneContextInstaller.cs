using Zenject;

namespace _Project.Scripts.Main.AppServices
{
    public class ArSceneContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ArControlService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}