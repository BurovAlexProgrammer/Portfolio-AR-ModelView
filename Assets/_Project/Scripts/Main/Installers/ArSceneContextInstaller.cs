using _Project.Scripts.Main.AppServices;
using Zenject;

namespace _Project.Scripts.Main.Installers
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