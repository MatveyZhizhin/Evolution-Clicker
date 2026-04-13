using UI;
using System;
using UnityEngine;
using Game;

namespace Money
{
    public class Balance : MonoBehaviour, ITextUser
    {
        [SerializeField] private int _balance;

        public event Action<string, string> OnDataChanged;

        private const string KEY_BALANCE = "Balance";

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

        public void InitializeTextView(TextView view)
        {
            if (view.GetDataKey() == KEY_BALANCE)
            {
                view.SetTextDirectly(_balance.ToString());
            }
        }
    }
}