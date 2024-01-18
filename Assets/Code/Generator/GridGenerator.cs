using Code.Systems;
using UnityEngine;
using Zenject;

namespace Code.Generator
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _cube;
        [SerializeField] private GameObject _cellPrefab;
        [SerializeField] private Transform _cellParent;
        [SerializeField] private float _offsetCell = 0.2f;
        
        private Vector3 _cellSize;
        private Vector2Int _gridSize;
        private LoadSystem _loadSystem;
        
        [Inject]
        private void Construct(LoadSystem loadSystem)
        {
            _loadSystem = loadSystem;
        }

        public Transform[,] GetGrid()
        {
            _cellSize = _cellPrefab.transform.localScale;
            _gridSize = _loadSystem.LevelSetting._gridSize;
            
            Transform[,] grid = new Transform[_gridSize.x, _gridSize.y];
            
            GenerateGrid(grid);
            GenerateCube();
            
            return grid;
        }
        
        private void GenerateGrid(Transform[,] grid)
        {
            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int z = 0; z < _gridSize.y; z++)
                {
                    Vector3 position = 
                        new Vector3(x * (_cellSize.x + _offsetCell), 0, z * (_cellSize.z + _offsetCell)) + transform.position;
                    GameObject cell = Instantiate(_cellPrefab, position, Quaternion.identity, _cellParent);
                    grid[x, z] = cell.transform;
                    cell.name = $"X: {x} Y: {z}";
                }
            }
        }

        private void GenerateCube()
        {
            float gridWidth = _gridSize.x * (_cellSize.x + _offsetCell);
            float gridHeight = _gridSize.y * (_cellSize.z + _offsetCell);
            
            Vector3 cubeSize = new Vector3(gridWidth + _offsetCell * 2, 1, gridHeight + _offsetCell * 2);
            Vector3 cubePosition 
                = new Vector3( transform.position.x +(gridWidth - _cellSize.x - _offsetCell) / 2 , -1, transform.position.z +(gridHeight - _cellSize.z - _offsetCell) / 2);
            
            Transform camera = Camera.main.transform;
            camera.position = new Vector3(cubePosition.x, camera.position.y, camera.position.z);
            
            _cube.transform.localScale = cubeSize;
            _cube.transform.position = cubePosition;
        }
    }
}