using System;
using _Project.Scripts.SignalBusAndSignal;
using _Project.Scripts.Tower.Data;
using _Project.Scripts.Tower.PlaceTower;
using _Project.Scripts.WalletSystem;
using Zenject;

namespace _Project.Scripts.GameLoop
{
    public class GameManager : IInitializable, IDisposable
    {
        [Inject]
        private TowerPlacer _towerPlacer;
        
        [Inject]
        private TowerShop _towerShop;
        
        [Inject]
        private  Wallet _wallet;
        
        [Inject]
        private SignalBus _signalBus;

        public void BuyTower(TowerConfig towerConfig)
        {
            if (towerConfig==null)
                return;
            if (_towerPlacer.HasEmptyPlace())
            {
                if ( _towerShop.BuyTower(towerConfig) )
                {
                    _towerPlacer.PlaceTower(towerConfig);     
                }  
            }
        }

        public void Dispose()
        {
            _signalBus.UnsubscribeId<int>(SignalID.ADD_COIN, _wallet.AddValue);
        }

        public void Initialize()
        {
            _signalBus.SubscribeId<int>(SignalID.ADD_COIN , _wallet.AddValue);
        }
    }

    
}