using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Skins
{
    public class Skin : MonoBehaviour
    {
        [SerializeField] private bool _isBought;
        [SerializeField] private bool _isSelected;

        [SerializeField] private Image _background;
        [SerializeField] private string _id;

        public UnityEvent OnSkinPurchase;
        public UnityEvent OnSkinSelection;

        public bool IsBought { get => _isBought; set => _isBought = value; }
        public bool IsSelected { get => _isSelected; set => _isSelected = value; }
        public string Id => _id;

        public void Select(ref Image image) 
        {
            _isSelected = true;
            image.sprite = _background.sprite;
            OnSkinSelection.Invoke();
        }

        public void Remove()
        {
            _isSelected = false;
            if(_isBought)
                OnSkinPurchase?.Invoke();
        }

        public void Buy()
        {
            _isBought = true;
            OnSkinPurchase?.Invoke();
        }
    }
}
