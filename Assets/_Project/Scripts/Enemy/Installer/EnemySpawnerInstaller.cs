using UnityEngine;
using Zenject;

namespace _Project.Scripts.Enemy.Installer
{
    public class EnemySpawnerInstaller: MonoInstaller
    {
        [SerializeField] private EnemyFacade _baseEnemy;
        [SerializeField] private Transform _enemyPoolParent;
        public override void InstallBindings()
        {
            Container.BindMemoryPool<EnemyFacade, EnemyPool>()
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_baseEnemy)
                .UnderTransform(_enemyPoolParent);
        } 
    }
}