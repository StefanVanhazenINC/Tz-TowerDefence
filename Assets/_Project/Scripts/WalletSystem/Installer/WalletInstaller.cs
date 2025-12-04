using UnityEngine;
using Zenject;

namespace _Project.Scripts.WalletSystem.Installer
{
    public class WalletInstaller  : MonoInstaller
    {
        [SerializeField] private int _startCoin = 4;
        public override void InstallBindings()
        {
            Container.Bind<Wallet>().AsSingle().WithArguments( _startCoin).NonLazy();
        }
    }
}