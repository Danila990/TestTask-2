using System.Collections.Generic;
using System.Linq;
using Code.Enum;
using Code.Fruits;
using Code.Fruits.Basket;
using Code.Manager;
using Code.Systems;
using UnityEngine;
using Zenject;

namespace Code.Generator
{
    public class FruitGenerator : MonoBehaviour
    {
        [SerializeField] private FruitSpawnData[] _fruitSpawnDatas;
        
        [Header("Fruit")]
        [SerializeField] private float _offsetFruitY = 1f;
        [SerializeField] private Transform _fruitParent;
        
        [Header("Basket Fruit")]
        [SerializeField] private Transform _basketParent;
        [SerializeField] private BasketFruitButton _buttonBasket;
        
        private LoadSystem _loadSystem;
        private GridManager _gridManager;
        private BasketFruit _basketFruit;

        [Inject]
        private void Construct(LoadSystem loadSystem, GridManager gridManager, BasketFruit basketFruit)
        {
            _loadSystem = loadSystem;
            _gridManager = gridManager;
            _basketFruit = basketFruit;
        }
        
        public List<Fruit> GetFruits()
        {
            List<Fruit> fruits = new List<Fruit>(_loadSystem.LevelSetting._countFruits);
            
            foreach (FruitCell fruitCell in _loadSystem.LevelSetting.FruitCell)
            {
                FruitSpawnData currentFruitData = GetFruitData(fruitCell._type);

                Fruit spawnFruit = Instantiate(currentFruitData._fruit, _fruitParent);
                fruits.Add(spawnFruit);
                Vector3 offset = new Vector3(0, _offsetFruitY, 0);
                spawnFruit.transform.position = _gridManager.GetTransformCell(fruitCell._gridPosition).position + offset;
                spawnFruit.Init(fruitCell._type, fruitCell._gridPosition);
            }

            return fruits;
        }

        public List<BasketFruitButton> GetBasketFruits()
        {
            List<FruitType> sortTypesFruits = SortFruitTypes();
            List<BasketFruitButton> basketFruitButtons = new List<BasketFruitButton>();

            foreach (FruitType fruitType in sortTypesFruits)
            {
                FruitSpawnData currentFruitData = GetFruitData(fruitType);

                BasketFruitButton basketFruitButton = Instantiate(_buttonBasket, _basketParent);
                basketFruitButtons.Add(basketFruitButton);
                
                basketFruitButton.Init(fruitType, _basketFruit, currentFruitData._icon);
            }
            
            return basketFruitButtons;
        }

        public List<Sprite> GetFruitSprite()
        {
            List<Sprite> fruitSprite = new List<Sprite>();

            for (int i = 0; i < 3; i++)
            {
                fruitSprite.Add(_fruitSpawnDatas[i]._icon);
            }

            return fruitSprite;
        }
        
        private List<FruitType> SortFruitTypes()
        {
            List<FruitType> sortFruitTypes = new List<FruitType>();

            foreach (FruitCell fruitCell in _loadSystem.LevelSetting.FruitCell)
                sortFruitTypes.Add(fruitCell._type);
            
            var resultSortTypes = sortFruitTypes.Distinct().ToList();
            resultSortTypes.Sort();
            return resultSortTypes;
        }
        
        private FruitSpawnData GetFruitData(FruitType needFruitType)
        {
            foreach (FruitSpawnData fruitData in _fruitSpawnDatas)
            {
                if (fruitData._type == needFruitType)
                    return fruitData;
            }
            
            Debug.LogError($"{needFruitType} absent");
            return default;
        }
    }
}