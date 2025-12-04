
using System;
using System.Collections.Generic;
using _Project.Scripts.GameLoop;
using _Project.Scripts.Tower.Data;
using _Project.Scripts.WalletSystem;
using Zenject;

namespace _Project.Scripts.Ui.TowerHolderUi
{
    public class TowerHolderPresenter : IDisposable, IInitializable
    {
        private TowerHolder _towerHolder;
        private GameManager _gameManager;
        private TowerHolderView _towerHolderView;
        private Wallet _wallet;
        private Dictionary<TowerHolderView,TowerConfig> _towersViews = new Dictionary<TowerHolderView,TowerConfig> ();
        private ITowerHolderViewFactory _viewFactory;
        
        [Inject]
        public TowerHolderPresenter( Wallet wallet,TowerHolder towerHolder,GameManager gameManager, TowerHolderView towerHolderView,ITowerHolderViewFactory viewFactory )
        {
            _gameManager = gameManager;
            _towerHolder = towerHolder;
            _towerHolderView = towerHolderView;
            _wallet = wallet;
            _viewFactory = viewFactory;
        }
        public void Initialize()
        {
            _wallet.OnScoreAddedNotArg += CheckCostTower;
            _wallet.OnScoreRemoveNotArg  +=CheckCostTower;
            CreateTowerView();
        }

        public void CreateTowerView()
        {
            List<TowerConfig> towerConfigs = _towerHolder.GetTowerConfigs();
            
            for (int i = 0; i < towerConfigs.Count; i++)
            {
                TowerHolderView tempHolderView = _viewFactory.Create(towerConfigs[i]);
                _towersViews.Add(tempHolderView ,towerConfigs[i]);
                tempHolderView.Setup(towerConfigs[i].TowerClass.Visual,_towerHolder.GetCost(towerConfigs[i]).ToString());
                tempHolderView.AddActionOnClick(()=>BuyTower(tempHolderView));
            }
            CheckCostTower();
        }

        public void BuyTower(TowerHolderView view)
        {
            _gameManager.BuyTower(_towersViews.TryGetValue(view, out TowerConfig towerConfig) ? towerConfig : null);    
        }

        public void CheckCostTower()
        {
            foreach (var pair in _towersViews )
            {
                if (_wallet.CheckValue(_towerHolder.GetCost(pair.Value)))
                {
                    pair.Key.SetInteractable(true);    
                }
                else
                {
                    pair.Key.SetInteractable(false);    
                }
            }     
        }

        public void Dispose()
        {
            _wallet.OnScoreAddedNotArg -= CheckCostTower;
            _wallet.OnScoreRemoveNotArg -=CheckCostTower;
            _towersViews.Clear();
        }

       
    }
}