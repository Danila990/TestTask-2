using UnityEngine;
using Zenject;

namespace Code.ZenjectInstallers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private LoadSystem _loadSystem;
        [SerializeField] private SaveSystem _saveSystem;
        
        public override void InstallBindings()
        {
            Container.Bind<LoadSystem>().FromInstance(_loadSystem).AsSingle().NonLazy();
            
            Container.Bind<SaveSystem>().FromInstance(_saveSystem).AsSingle().NonLazy();
        }
    }
}