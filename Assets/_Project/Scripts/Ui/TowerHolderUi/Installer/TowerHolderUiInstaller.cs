using UnityEngine;
using Zenject;

namespace _Project.Scripts.Ui.TowerHolderUi.Installer
{
    public class TowerHolderUiInstaller  : MonoInstaller
    {
        [SerializeField] private TowerHolderView _prefabView;
        [SerializeField] private Transform _uiParent;
        public override void InstallBindings()
        {
            Container.BindInstance(_prefabView);
            Container.Bind<ITowerHolderViewFactory>().To<TowerHolderViewFactory>().AsSingle().WithArguments(_uiParent).NonLazy();  
            Container.BindInterfacesAndSelfTo<TowerHolderPresenter>().AsSingle().NonLazy();
        }
    }
}