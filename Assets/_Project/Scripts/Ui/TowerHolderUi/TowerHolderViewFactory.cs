using _Project.Scripts.Tower;
using _Project.Scripts.Tower.Data;
using _Project.Scripts.Tower.Factory;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Ui.TowerHolderUi
{
    public interface ITowerHolderViewFactory : IFactory<TowerConfig, TowerHolderView> { }
    public class TowerHolderViewFactory   :  ITowerHolderViewFactory 
    {
        private readonly DiContainer _container;
        private TowerHolderView _basePrefab;
        private Transform _parent;

        [Inject]
        public TowerHolderViewFactory(DiContainer container,TowerHolderView  basePrefab, Transform parent)
        {
            _container = container;
            _basePrefab = basePrefab;
            _parent = parent;
        }

        public TowerHolderView Create(TowerConfig param)
        {   
            TowerHolderView view = _container.InstantiatePrefabForComponent<TowerHolderView>(_basePrefab);
            view.transform.SetParent(_parent);
            view.transform.localScale = Vector3.one;
            return  view;
        }
    }
  
}