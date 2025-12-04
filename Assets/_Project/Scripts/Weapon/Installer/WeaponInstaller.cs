using _Project.Scripts.Weapon.Factory;
using _Project.Scripts.Weapon.ProjectileBase;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Weapon.Installer
{
    public class WeaponInstaller : MonoInstaller
    {
        [SerializeField] private int _maxSizePool = 1000;
        [SerializeField] private Transform _parentPool;
        [SerializeField] private BaseWeapon _weapon;    
        
        
        public override void InstallBindings()
        {
            InstallPool();
            InstallWeapon();
        }

        private void InstallPool()
        {
            Container.BindInterfacesAndSelfTo<ProjectilePool>().AsSingle().WithArguments( _parentPool, _maxSizePool).NonLazy();
        }

        private void InstallWeapon()
        {
            Container.Bind<BaseWeapon>().FromInstance(_weapon).AsSingle();
            Container.BindInterfacesAndSelfTo<BaseWeaponConfigurator>().AsSingle().NonLazy();
            Container.Bind<IWeaponFactory>().To<WeaponFactory>().AsSingle().NonLazy();
        }
    }
}