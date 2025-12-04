using System;
using System.Collections.Generic;
using _Project.Scripts.Tower.Data;
using Alchemy.Serialization;
using UnityEngine;

namespace _Project.Scripts.GameLoop
{
    [CreateAssetMenu(menuName = "Tower/New Tower Catalog", fileName = "Tower Catalog")]
    [AlchemySerialize]
    [ShowAlchemySerializationData]
    public  partial class TowerCatalogConfig : ScriptableObject
    {
       [AlchemySerializeField, NonSerialized]public  Dictionary<TowerConfig,int> _towerCatalog = new Dictionary<TowerConfig,int>();  

        public Dictionary<TowerConfig,int> TowerCatalog => _towerCatalog;

        public int GetCost(TowerConfig config)
        {
            return _towerCatalog[config];
        }

        public List<TowerConfig> GetTowerConfigs()
        {
            List<TowerConfig> towerConfigs = new List<TowerConfig>();
            foreach (var pair in _towerCatalog)
            {
                towerConfigs.Add( pair.Key);  
            }
            return towerConfigs;
        }
    }
}