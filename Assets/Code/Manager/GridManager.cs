using Code.Enum;
using Code.Generator;
using UnityEngine;
using Zenject;

namespace Code.Manager
{
    public class GridManager : MonoBehaviour
    {
        public int RangeGridX => _transformCells.GetLength(0);
        public int RangeGridY => _transformCells.GetLength(1);
        
        private Transform[,] _transformCells;
        private GridGenerator _gridGenerator;
        
        [Inject]
        private void Construct(GridGenerator gridGenerator)
        {
            _gridGenerator = gridGenerator;
        }

        private void Awake()
        {
            _transformCells = _gridGenerator.GetGrid();
        }

        public Transform GetTransformCell(Vector2Int index) => _transformCells[index.x, index.y];

        public Vector2Int GetNextCellIndex(DirectionMove directionMove, Vector2Int currentIndex)
        {
            if (directionMove == DirectionMove.Up)
            {
                if (RangeGridY - 1 > currentIndex.y)
                    currentIndex = new Vector2Int(currentIndex.x, currentIndex.y + 1);
            }
            else if (directionMove == DirectionMove.Down)
            {
                if (0 < currentIndex.y)
                    currentIndex = new Vector2Int(currentIndex.x, currentIndex.y - 1);
            }
            else if (directionMove == DirectionMove.Left)
            {
                if (0 < currentIndex.x)
                    currentIndex = new Vector2Int(currentIndex.x - 1, currentIndex.y);
            }
            else if (directionMove == DirectionMove.Right)
            {
                if (RangeGridX - 1 > currentIndex.x)
                    currentIndex = new Vector2Int(currentIndex.x + 1, currentIndex.y);
            }

            return currentIndex;
        }
    }
}