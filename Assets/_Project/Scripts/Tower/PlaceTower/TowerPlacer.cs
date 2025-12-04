using System.Collections.Generic;
using _Project.Scripts.Tower.Data;
using _Project.Scripts.Tower.Factory;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Tower.PlaceTower
{
    public class TowerPlacer 
    {
        private List<TowerPlace> _towerPlaces ;
        private List<TowerPlace> _emptyPlaces;

        private ITowerFactory _towerFactory;

        [Inject]
        public TowerPlacer(ITowerFactory towerFactory)
        {
            _towerPlaces = new List<TowerPlace>();
            _emptyPlaces= new List<TowerPlace>();
            _towerFactory = towerFactory;
        }

        public bool HasEmptyPlace()
        {
            return _emptyPlaces.Count>0 ? true : false;   
        }

        public void AddTowerPlace(TowerPlace towerPlace)
        {
            _towerPlaces.Add(towerPlace);
            _emptyPlaces.Add(towerPlace);
        }

        public void PlaceTower(TowerConfig towerConfig)
        {
            int randomPlace = UnityEngine.Random.Range(0, _emptyPlaces.Count);
            if ( _emptyPlaces[randomPlace].HasEmpty)
            {
                TowerFacade tempFacade = _towerFactory.Create(towerConfig);
                _emptyPlaces[randomPlace].SetTowerPlace(tempFacade);
                
                _emptyPlaces.Remove(_emptyPlaces[randomPlace]);
            }
         
        }
    }
}