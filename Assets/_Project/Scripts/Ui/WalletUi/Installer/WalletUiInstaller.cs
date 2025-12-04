using UnityEngine;
using Zenject;

namespace _Project.Scripts.Ui.WalletUi.Installer
{
    public class WalletUiInstaller : MonoInstaller
    {
        [SerializeField] private WalletView _walletView;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<WalletPresenter>().AsSingle().WithArguments(_walletView).NonLazy();
        }
    }
}