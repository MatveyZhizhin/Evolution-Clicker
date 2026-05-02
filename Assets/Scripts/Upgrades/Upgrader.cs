<<<<<<< Updated upstream
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
=======
using Game;
using Money;
using System;
using UI.ProgressBar;
using UI.Text;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Upgrades
{
    public abstract class Upgrader : MonoBehaviour, ITextUser, IProgressBarUser
    {
        [SerializeField] protected long _cost;
        [SerializeField] private int _costMultiplier;
        [SerializeField] protected int _value;
        [SerializeField] protected int _maxLevel;
        [SerializeField] private UnityEvent _onMaxLevel;

        protected Balance _balance;

        private const string KEY_COST = "Cost";
        private const string KEY_LEVEL = "Level";
        private const string KEY_UPGRADE = "Upgrade";

        public event Action<string, string> OnDataChanged;
        public event Action<int, int> OnValueChanged;

        private int _currentLevel = 0;

        public int Level { get => _currentLevel; set => _currentLevel = value; }
        public long Cost { get => _cost; set => _cost = value; }
>>>>>>> Stashed changes

        private void Awake()
        {
            _balance = FindObjectOfType<Balance>();
        }

<<<<<<< Updated upstream
        public abstract void Upgrade();
=======
        private void Start()
        {
            DisplayText(StringParser.ParseFloatToShortString(_cost, 1), KEY_COST);
            DisplayText(GetDisplayableText(), KEY_UPGRADE);
            OnValueChanged?.Invoke(_currentLevel, _maxLevel);
            OnDataChanged?.Invoke(KEY_LEVEL, $"{_currentLevel}/{_maxLevel}");
        }

        public virtual void HandleUpgrade()
        {
            if (_balance.HasMoney(_cost) && !IsMaxLevel())
            {
                _balance.DecreaseBalance(_cost);
                _cost *= _costMultiplier;
                Upgrade();
                DisplayText(GetDisplayableText(), KEY_UPGRADE);
                DisplayText(StringParser.ParseFloatToShortString(_cost, 1), KEY_COST);
            }

            if (IsMaxLevel())
            {
                DisplayText("Макс", KEY_COST);
                DisplayText("Макс", KEY_UPGRADE);
                _onMaxLevel.Invoke();
                return;
            }
        }

        protected abstract string GetDisplayableText();

        private bool IsMaxLevel() => _currentLevel >= _maxLevel;

        protected void DisplayText(string text, string key)
        {
            OnDataChanged?.Invoke(key, text);
        }

        protected virtual void Upgrade()
        {
            _currentLevel++;
            OnValueChanged?.Invoke(_currentLevel, _maxLevel);
            OnDataChanged?.Invoke(KEY_LEVEL, $"{_currentLevel}/{_maxLevel}");
        }
>>>>>>> Stashed changes
    }
}
