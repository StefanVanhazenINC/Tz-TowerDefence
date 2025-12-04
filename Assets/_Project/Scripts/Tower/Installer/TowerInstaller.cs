using _Project.Scripts.Tower.Factory;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Tower.Installer
{
    public class TowerInstaller  : MonoInstaller
    {
        [SerializeField] private TowerFacade _baseTower;
        public override void InstallBindings()
        {
            Container.Bind<ITowerFactory>().To<TowerFactory>().AsSingle().WithArguments(_baseTower).NonLazy();
        }
    }
}