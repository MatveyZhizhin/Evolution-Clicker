using YG;
using UnityEngine;
using Money;
using Game;

namespace Purchases
{
    public class MoneyPurchaseProvider : MonoBehaviour
    {
        private Balance _balance;
        private SaveManager _saveManager;

        private void Awake()
        {
            _balance = FindObjectOfType<Balance>();    
            _saveManager = FindObjectOfType<SaveManager>();
        }

        protected void Purchase(string id)
        {     
            if (int.Parse(id) < 100) return;

            _balance.IncreaseBalance(int.Parse(id));
            _saveManager.Save();
        }

        private void OnEnable()
        {
            YandexGame.PurchaseSuccessEvent += Purchase;
        }
        private void OnDisable()
        {
            YandexGame.PurchaseSuccessEvent -= Purchase;
        }
    }
}
