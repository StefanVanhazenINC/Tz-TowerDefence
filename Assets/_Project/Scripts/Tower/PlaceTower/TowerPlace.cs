using UnityEngine;
using Zenject;

namespace _Project.Scripts.Tower.PlaceTower
{
    public class TowerPlace  : MonoBehaviour
    {
        [SerializeField] private Transform _center;
        
        private TowerFacade _towerFacade;
        
        public bool HasEmpty => _towerFacade == null;
        
        [Inject]
        public void Construct(TowerPlacer towerPlacer)
        {
            towerPlacer.AddTowerPlace(this);
        }

        public void  SetTowerPlace( TowerFacade towerFacade)
        {
            _towerFacade = towerFacade;
            towerFacade.transform.position = _center.position;
        }

    }
}