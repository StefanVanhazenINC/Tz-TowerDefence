using _Project.Scripts.Tower.Data;
using Zenject;

namespace _Project.Scripts.Tower.Factory
{
    public interface ITowerFactory : IFactory<TowerConfig, TowerFacade> { }
    public class TowerFactory  : ITowerFactory
    {
        private readonly DiContainer _container;
        private TowerFacade _basePrefab;

        [Inject]
        public TowerFactory(DiContainer container, TowerFacade basePrefab)
        {
            _container = container;
            _basePrefab = basePrefab;
        }

        public TowerFacade Create(TowerConfig param)
        {   
            TowerFacade tower = _container.InstantiatePrefabForComponent<TowerFacade>(_basePrefab);
            tower.Create(param);
            return tower;
        }
    }
}