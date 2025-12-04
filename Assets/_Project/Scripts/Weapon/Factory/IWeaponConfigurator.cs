using _Project.Scripts.Weapon.Data;
using _Project.Scripts.Weapon.ProjectileBase;
using Zenject;

namespace _Project.Scripts.Weapon.Factory
{
    public interface IWeaponConfigurator
    {
        public BaseWeapon Install(DiContainer container,WeaponConfig config, IPoolProjectile pool, BaseWeapon weapon); 
    }
}