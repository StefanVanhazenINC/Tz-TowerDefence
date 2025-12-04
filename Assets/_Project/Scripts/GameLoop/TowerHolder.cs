using System.Collections.Generic;
using _Project.Scripts.Tower.Data;
using Zenject;

namespace _Project.Scripts.GameLoop
{
    public class TowerHolder
    {
        
        [Inject]
        private TowerCatalogConfig _towerCatalogConfig;
        public List<TowerConfig> GetTowerConfigs()  => _towerCatalogConfig.GetTowerConfigs(); 
        public int GetCost(TowerConfig config) => _towerCatalogConfig.GetCost(config);  
    }
}