using UI.Text;
using System;
using UnityEngine;
using Game;

namespace Money
{
    public class Balance : MonoBehaviour, ITextUser
    {
        [SerializeField] private int _balance;

        [field: SerializeField] public int ClickRewardMultiplier { get; set; } = 1;
        [field: SerializeField] public int PassiveRewardMultiplier { get; set; } = 1;

        public event Action<string, string> OnDataChanged;

        private const string KEY_BALANCE = "Balance";

        public bool HasMoney(int value) => _balance >= value;

        private void OnEnable()
        {
            OnDataChanged?.Invoke(KEY_BALANCE, StringParser.ParseFloatToShortString(_balance, 2));
        }

        public void IncreaseBalance(int value)
        {
            _balance += value;
            OnDataChanged?.Invoke(KEY_BALANCE, StringParser.ParseFloatToShortString(_balance, 2));
        }

        public void DecreaseBalance(int value)
        {
            _balance -= value;
            OnDataChanged?.Invoke(KEY_BALANCE, StringParser.ParseFloatToShortString(_balance, 2));
        }
    }
}