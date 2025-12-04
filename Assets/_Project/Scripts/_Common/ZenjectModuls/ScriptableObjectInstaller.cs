namespace _Project.Scripts._Common.ZenjectModuls
{
    using UnityEngine;
    using Zenject;
    [CreateAssetMenu(fileName = "ScriptableObjectInstaller", menuName = "Installers/ScriptableObjectInstaller")]
    public class ScriptableObjectInstaller : ScriptableObjectInstaller<ScriptableObjectInstaller>
    {
        public ScriptableObject[] configs;
        public override void InstallBindings()
        {
            foreach (var config in configs)
            {
                Container.Bind(config.GetType()).FromInstance(config).AsSingle();
            }
        }
    }
}