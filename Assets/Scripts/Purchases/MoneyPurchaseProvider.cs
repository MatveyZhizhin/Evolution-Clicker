using YG;
using UnityEngine;
using Money;

namespace Purchases
{
    public class MoneyPurchaseProvider : MonoBehaviour
    {
        private Balance _balance;

        private void Awake()
        {
            _balance = FindObjectOfType<Balance>();     
        }

        protected void Purchase(string id)
        {     
            if (int.Parse(id) < 100) return;

            _balance.IncreaseBalance(int.Parse(id));
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
