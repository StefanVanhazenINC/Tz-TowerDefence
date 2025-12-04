using _Project.Scripts.Weapon.Data;
using _Project.Scripts.Weapon.ProjectileBase;

namespace _Project.Scripts.Weapon.Base
{
    public interface IShoter
    {
        public void Shot(WeaponContext context);

        public IShoter Clone(IPoolProjectile pool);
        
        
        
    
    }
}