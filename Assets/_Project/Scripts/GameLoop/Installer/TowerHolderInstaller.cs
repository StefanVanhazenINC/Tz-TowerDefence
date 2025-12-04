using _Project.Scripts.Ui.TowerHolderUi;
using _Project.Scripts.Weapon.Factory;
using Zenject;

namespace _Project.Scripts.GameLoop.Installer
{
    public class TowerHolderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
           Container.Bind<TowerHolder>().AsSingle().NonLazy();
        }
    }
}