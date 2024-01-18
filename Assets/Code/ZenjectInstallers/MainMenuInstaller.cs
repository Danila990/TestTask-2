using Code.Generator;
using Code.Level;
using Code.Manager;
using Code.UI;
using UnityEngine;
using Zenject;

namespace Code.ZenjectInstallers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private LevelButtonManager _levelButtonManager;
        [SerializeField] private LevelsConstructor _levelsConstructor;
        [SerializeField] private LevelButtonGenerator _levelButtonGenerator;
        [SerializeField] private Coins _coins;
    
        public override void InstallBindings()
        {
            Container.Bind<LevelButtonManager>().FromInstance(_levelButtonManager).AsSingle().NonLazy();
            
            Container.Bind<LevelsConstructor>().FromInstance(_levelsConstructor).AsSingle().NonLazy();
            
            Container.Bind<LevelButtonGenerator>().FromInstance(_levelButtonGenerator).AsSingle().NonLazy();
            
            Container.Bind<Coins>().FromInstance(_coins).AsSingle().NonLazy();
        }
    }
}