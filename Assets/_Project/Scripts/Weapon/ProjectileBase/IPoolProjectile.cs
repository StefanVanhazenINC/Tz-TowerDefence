namespace _Project.Scripts.Weapon.ProjectileBase
{
    
    public interface IPoolProjectile
    {
        public IProjectile GetObjectInPool(Projectile prefab);
    }
}