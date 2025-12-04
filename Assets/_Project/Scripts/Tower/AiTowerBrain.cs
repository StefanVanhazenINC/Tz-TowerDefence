using UnityEngine;

namespace _Project.Scripts.Tower
{
    public class AiTowerBrain  : MonoBehaviour
    {
        [SerializeField] private TowerFacade _towerFacade;
    
        private void Update()
        {               
            if (!_towerFacade.WeaponNull)
            {
                if (_towerFacade.TargetNotNull)
                {
                    _towerFacade.AimToTarget();
                    if (_towerFacade.CheckReadyWeapon)
                    {
                        _towerFacade.UseWeapon();
                    }
                }
            }
        } 
        
        
    }
}