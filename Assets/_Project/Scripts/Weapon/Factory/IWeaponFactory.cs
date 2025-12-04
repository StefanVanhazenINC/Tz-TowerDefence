using _Project.Scripts.Weapon.Data;
using _Project.Scripts.Weapon.ProjectileBase;
using Zenject;

namespace _Project.Scripts.Weapon.Factory
{
    public interface IWeaponFactory : IFactory<WeaponConfig, BaseWeapon> { }
    public class WeaponFactory : IWeaponFactory
    {
        private readonly DiContainer _container;
        private BaseWeapon _prefab;
        private IPoolProjectile _pool;
        private IWeaponConfigurator _weaponConfigurator;
        
        
        [Inject]
        public WeaponFactory(DiContainer container, BaseWeapon prefab, IPoolProjectile pool, IWeaponConfigurator configurator)
        {

            _container = container;
            _prefab = prefab;
            _pool = pool;
            _weaponConfigurator = configurator;

        }

        
        public BaseWeapon Create(WeaponConfig param)
        {
            var weapon = _container.InstantiatePrefabForComponent<BaseWeapon>(_prefab);
            weapon = _weaponConfigurator.Install(_container, param, _pool, weapon);
            return weapon; 
        }
    }
}