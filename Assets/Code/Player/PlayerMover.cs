using System;
using Code.Enum;
using Code.Manager;
using UnityEngine;
using Zenject;

namespace Code.Player
{
    public class PlayerMover : MonoBehaviour
    {
        public event Action<Vector2Int> OnTriggerCell;
        
        [SerializeField] private float _movementSpeed;
        
        private Vector2Int _currentCellIndex;
        private DirectionMove _currentDirectionMove;
        private bool _isStop = false;
        private Transform _targetCellTransform;
        
        private GridManager _gridManager;
        private LoadSystem _loadSystem;
        private InputSwipe _inputSwipe;
        private GameManager _gameManager;

        [Inject]
        private void Construct(GridManager gridManager, LoadSystem loadSystem, InputSwipe inputSwipe
        ,GameManager gameManager)
        {
            _loadSystem = loadSystem;
            _gridManager = gridManager;
            _inputSwipe = inputSwipe;
            _gameManager = gameManager;
        }

        private void Start()
        {
            RestartMover();
        }

        private void OnEnable()
        {
            _inputSwipe.OnSwipe += ChangeDirection;
            _gameManager.OnRestartGame += RestartMover;
        }

        private void OnDisable()
        {
            _gameManager.OnRestartGame -= RestartMover;
            _inputSwipe.OnSwipe -= ChangeDirection;
        }

        private void Update()
        {
            if (!_gameManager._isPlayGame) return;
            
            Movement();
        }

        private void Movement()
        {
            if (Vector3.Distance(transform.position,_targetCellTransform.position) >= 0.2f)
            {
                Vector3 directionMove = _targetCellTransform.position - transform.position;
                transform.Translate(directionMove.normalized * (_movementSpeed * Time.deltaTime),Space.World);
            }
            else if (_isStop == false)
            {
                OnTriggerCell?.Invoke(_currentCellIndex);
                FindNextCell();
            }
        }
        
        private void FindNextCell()
        {
            Vector2Int targetCellIndex = _gridManager.GetNextCellIndex(_currentDirectionMove, _currentCellIndex);

            if (targetCellIndex == _currentCellIndex)
            {
                _isStop = true;
                return;
            }

            _currentCellIndex = targetCellIndex;
            _targetCellTransform = _gridManager.GetTransformCell(targetCellIndex);
            _isStop = false;
        }
        
        private void ChangeDirection(DirectionMove directionMove)
        {
            if (_currentDirectionMove == directionMove) return;
            
            _currentDirectionMove = directionMove;
            _isStop = false;
        }

        private void RestartMover()
        {
            _currentDirectionMove = _loadSystem.LevelSetting._startPlayerDirection;
            Vector2Int startPosition = _loadSystem.LevelSetting._startPlayerPosition;
            
            _currentCellIndex = startPosition;
            transform.position = _gridManager.GetTransformCell(startPosition).position;
            
            _targetCellTransform = _gridManager.GetTransformCell(_currentCellIndex);

            _isStop = false;
        }
    }
}