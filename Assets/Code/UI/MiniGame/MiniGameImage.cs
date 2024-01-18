using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.MiniGame
{
    public class MiniGameImage : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private MiniGame _miniGame;
        
        public Image _image { get; private set; }
        
        private Sprite _openSprite;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetupOpenSprite(Sprite sprite)
        {
            GetComponent<Image>().sprite = null;
            _openSprite = sprite;
        }

        public void OpenSprite()
        {
            _image.sprite = _openSprite;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _miniGame.ButtonImageClick(this);
        }
    }
}