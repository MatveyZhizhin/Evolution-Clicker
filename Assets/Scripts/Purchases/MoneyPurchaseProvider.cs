using YG;
using UnityEngine;
using Money;

namespace Purchases
{
    public class MoneyPurchaseProvider : MonoBehaviour
    {
        [SerializeField] private string _id;

        private Balance _balance;

        private void Awake()
        {
            _balance = FindObjectOfType<Balance>();     
        }

        protected void Purchase(string id)
        {     
            if (id != _id) return;

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
