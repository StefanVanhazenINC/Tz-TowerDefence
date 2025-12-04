using UnityEngine;

namespace _Project.Scripts.Tower.Data
{
    [CreateAssetMenu(menuName = "Tower/New Tower Config", fileName = "TowerConfig")]
    public class TowerConfig : ScriptableObject
    {
        [SerializeField] private TowerClass _towerClass;  
        public TowerClass TowerClass => _towerClass;
        
    }
}