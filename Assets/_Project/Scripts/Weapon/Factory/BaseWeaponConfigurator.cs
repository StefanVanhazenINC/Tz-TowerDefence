using _Project.Scripts.Weapon.Base;
using _Project.Scripts.Weapon.Data;
using _Project.Scripts.Weapon.ProjectileBase;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Weapon.Factory
{
    public class BaseWeaponConfigurator  : IWeaponConfigurator 
    {
        
        public BaseWeapon Install(DiContainer container, WeaponConfig config, IPoolProjectile pool, BaseWeapon weapon)
        {
            ShoterInstaller(config,weapon,pool,out IShoter shoter); 
            SetWeaponVisualContainer(config,weapon,out WeaponVisual visual);
            weapon.Set(shoter, config,visual);
            return weapon;
        }

        public void ShoterInstaller(WeaponConfig config,BaseWeapon weapon, IPoolProjectile pool, out IShoter shot)
        {
            shot = config.WeaponData.ShoterPrototype.Clone(pool);
        }
        private void  SetWeaponVisualContainer(WeaponConfig config, BaseWeapon weapon, out WeaponVisual weaponVisual)
        {
            weaponVisual = GameObject.Instantiate(config.WeaponVisual, weapon.transform);
        }

        
    }
}