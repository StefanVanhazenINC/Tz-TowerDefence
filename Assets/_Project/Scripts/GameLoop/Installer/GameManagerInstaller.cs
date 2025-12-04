using Zenject;

namespace _Project.Scripts.GameLoop.Installer
{
    public class GameManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        { 
            
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
        }
    }
}