using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Skins
{
    public class SkinShop : MonoBehaviour
    {
        [SerializeField] private List<Skin> _skins;

        [SerializeField] private Image _background;

        private void Start()
        {
            foreach (var skin in _skins)
            {
                if (skin.IsBought)
                    skin.Buy();

                if (skin.IsSelected)
                    skin.Select(ref _background);
                else 
                    skin.Remove();
            }
        }

        public void ChangeSkin(int index)
        {
            foreach (var skin in _skins)
            {
                if (skin != _skins[index])
                {
                    if (skin.IsSelected && _skins[index].IsBought)
                    {
                        skin.Remove();
                        _skins[index].Select(ref _background);
                    }
                }    
            }
        }

        public void BuySkin(string id)
        {    
            foreach (var skin in _skins)
            {
                if (skin.Id == id)
                {
                    skin.Buy();
                    ChangeSkin(_skins.IndexOf(skin));
                }
            }          
        }

        private void OnEnable()
        {
            YandexGame.PurchaseSuccessEvent += BuySkin;
        }

        private void OnDisable()
        {
            YandexGame.PurchaseSuccessEvent -= BuySkin;
        }
    }
}