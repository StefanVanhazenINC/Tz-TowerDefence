using Zenject;

namespace _Project.Scripts.GameLoop.Installer
{
    public class TowerShopInstaller  : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TowerShop>().AsSingle().NonLazy();
        }
    }
}