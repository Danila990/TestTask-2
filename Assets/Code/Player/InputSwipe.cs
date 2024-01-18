using System;
using Code.Enum;
using UnityEngine;

namespace Code.Player
{
    public class InputSwipe : MonoBehaviour
    {
        public event Action<DirectionMove> OnSwipe;
        
        [SerializeField] private int _minDragingRange = 125;
        [SerializeField] private int _maxDragingRange = 500;

        private Vector2 _startTouch;
        private Vector2 _swipeDelta;

        private void Update()
        {
            Inputs();
        }

        private void Inputs()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _swipeDelta = Vector2.zero;
                _swipeDelta = (Vector2)Input.mousePosition - _startTouch;

                Swipe();

                _startTouch = Vector2.zero;
                _swipeDelta = Vector2.zero;
            }
        }

        private void Swipe()
        {
            if(_swipeDelta.magnitude > _minDragingRange && _swipeDelta.magnitude < _maxDragingRange)
            {
                float x = _swipeDelta.x;
                float y = _swipeDelta.y;
                
                DirectionMove directionMove = DirectionMove.Up;
                
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x < 0)
                        directionMove = DirectionMove.Left;
                    else
                        directionMove = DirectionMove.Right;
                }
                else
                {
                    if (y < 0)
                        directionMove = DirectionMove.Down;
                    else
                        directionMove = DirectionMove.Up;
                }
                OnSwipe?.Invoke(directionMove);
            }
        }
    }
}