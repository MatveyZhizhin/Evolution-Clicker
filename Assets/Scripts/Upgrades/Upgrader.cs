using Money;
using System;
using UI.Text;
using UnityEngine;

namespace Upgrades
{
    public abstract class Upgrader : MonoBehaviour, ITextUser
    {
        [SerializeField] private int _cost;
        [SerializeField] private int _costMultiplier;
        [SerializeField] private int _value;

        private Balance _balance;

        public event Action<string, string> OnDataChanged;

        private void Awake()
        {
            _balance = FindObjectOfType<Balance>();
        }

        public abstract void Upgrade();
    }
}
