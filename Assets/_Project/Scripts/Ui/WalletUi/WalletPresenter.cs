using System;
using _Project.Scripts.WalletSystem;
using Zenject;

namespace _Project.Scripts.Ui.WalletUi
{
    public class WalletPresenter : IInitializable, IDisposable
    {
        private Wallet _wallet;    
        private WalletView _walletView;

        [Inject]
        public WalletPresenter(WalletView walletView,Wallet wallet)
        {
            _wallet = wallet;
            _walletView = walletView;
            WalletChange(_wallet.Value);
        }

        public void WalletChange(int value)
        {
            _walletView.SetValue(_wallet.Value.ToString());        
        }

        public void Dispose()
        {
            _wallet.OnScoreAdded -= WalletChange;
            _wallet.OnScoreRemove -= WalletChange;
        }

        public void Initialize()
        {
            _wallet.OnScoreAdded += WalletChange;
            _wallet.OnScoreRemove += WalletChange;
        }
    }
}