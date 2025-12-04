using Zenject;

namespace _Project.Scripts.SignalBusAndSignal
{
    public class DeclarateSignalBusInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<int>().WithId(SignalID.ADD_COIN);
        } 
    }
}