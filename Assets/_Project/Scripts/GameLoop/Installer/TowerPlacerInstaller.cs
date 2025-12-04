using _Project.Scripts.Tower.PlaceTower;
using Zenject;

namespace _Project.Scripts.GameLoop.Installer
{
    public class TowerPlacerInstaller  : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TowerPlacer>().AsSingle().NonLazy();
        }
    }                                            
}