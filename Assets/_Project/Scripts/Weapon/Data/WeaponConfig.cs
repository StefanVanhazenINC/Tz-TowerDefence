using UnityEngine;

namespace _Project.Scripts.Weapon.Data
{
    
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/new Weapon", order = 0)]
    public class WeaponConfig: ScriptableObject
    {
        [SerializeField] private WeaponData _weaponData;
        [SerializeField] private WeaponVisual _weaponVisual;
        
        public WeaponData WeaponData => _weaponData;
        public WeaponVisual WeaponVisual => _weaponVisual;
    }
}