using _Project.Scripts.Tower.Data;
using _Project.Scripts.WalletSystem;
using Zenject;

namespace _Project.Scripts.GameLoop
{
    public class TowerShop
    {
        [Inject]
        private Wallet _coinWallet;
        [Inject]
        private TowerCatalogConfig _towerCatalog;

      

        public bool BuyTower(TowerConfig towerConfig)
        {
            if (_coinWallet.TryRemoveValue(_towerCatalog.GetCost(towerConfig)))
            {
                return true;
            }
            return false;
        }
    }
}