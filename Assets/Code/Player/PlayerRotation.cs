using Code.Enum;
using Code.Manager;
using Code.Systems;
using UnityEngine;
using Zenject;

namespace Code.Player
{
    public class PlayerRotation : MonoBehaviour
    {
        private DirectionMove _directionMove;
        
        private LoadSystem _loadSystem;
        private InputSwipe _inputSwipe;
        private GameManager _gameManager;
        private PlayerMover _playerMover;

        [Inject]
        private void Construct(LoadSystem loadSystem, InputSwipe inputSwipe
            ,GameManager gameManager, PlayerMover playerMover)
        {
            _playerMover = playerMover;
            _loadSystem = loadSystem;
            _inputSwipe = inputSwipe;
            _gameManager = gameManager;
        }

        private void Start()
        {
            _directionMove = _loadSystem.LevelSetting._startPlayerDirection;
            PlayerRotate();
        }

        private void OnEnable()
        {
            _inputSwipe.OnSwipe += ChangeDirection;
            _gameManager.OnRestartGame += RestartRotation;
            _playerMover.OnTriggerCell += PlayerRotate;
        }

        private void OnDisable()
        {
            _inputSwipe.OnSwipe -= ChangeDirection;
            _gameManager.OnRestartGame -= RestartRotation;
            _playerMover.OnTriggerCell -= PlayerRotate;
        }

        private void RestartRotation()
        {
            ChangeDirection(_loadSystem.LevelSetting._startPlayerDirection);
            PlayerRotate();
        }
        
        private void ChangeDirection(DirectionMove directionMove)
        {
            _directionMove = directionMove;
        }

        private void PlayerRotate(Vector2Int cell = new Vector2Int())
        {
            if (_directionMove == DirectionMove.Up)
                Rotate(0);
            else if (_directionMove == DirectionMove.Down)
                Rotate(180);
            else if (_directionMove == DirectionMove.Left)
                Rotate(-90);
            else if (_directionMove == DirectionMove.Right)
                Rotate(90);
        }
        
        private void Rotate(float y) => transform.rotation = Quaternion.Euler(0, y, 0);
    }
}