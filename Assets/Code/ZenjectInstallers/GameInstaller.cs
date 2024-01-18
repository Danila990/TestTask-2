using Code.Fruits;
using Code.Fruits.Basket;
using Code.Generator;
using Code.Manager;
using Code.Player;
using Code.UI.MiniGame;
using UnityEngine;
using Zenject;

namespace Code.ZenjectInstallers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private BasketFruit _basketFruit;
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private InputSwipe inputSwipe;
        [SerializeField] private GridGenerator _gridGenerator;
        [SerializeField] private FruitGenerator _fruitGenerator;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private FruitManager _fruitManager;
    
        public override void InstallBindings()
        {
            Container.Bind<BasketFruit>().FromInstance(_basketFruit).AsSingle().NonLazy();
            
            Container.Bind<PlayerMover>().FromInstance(_playerMover).AsSingle().NonLazy();
            
            Container.Bind<InputSwipe>().FromInstance(inputSwipe).AsSingle().NonLazy();
            
            Container.Bind<GridGenerator>().FromInstance(_gridGenerator).AsSingle().NonLazy();
            
            Container.Bind<FruitGenerator>().FromInstance(_fruitGenerator).AsSingle().NonLazy();
            
            Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle().NonLazy();
            
            Container.Bind<GridManager>().FromInstance(_gridManager).AsSingle().NonLazy();
            
            Container.Bind<FruitManager>().FromInstance(_fruitManager).AsSingle().NonLazy();
        }
    }
}