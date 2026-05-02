using UI.Text;
using System;
using UnityEngine;
using Game;
using Spawn;
using Evolution;

namespace Money
{
    public class Balance : MonoBehaviour, ITextUser
    {
        [SerializeField] private long _balance;
        [SerializeField] private EvolutionChain _chain;

        [field: SerializeField] public int ClickRewardMultiplier { get; set; } = 1;
        [field: SerializeField] public int PassiveRewardMultiplier { get; set; } = 1;

        public event Action<string, string> OnDataChanged;

        private const string KEY_BALANCE = "Balance";
        private const string KEY_PASSIVE_REWARD = "PassiveReward";
        public bool HasMoney(long value) => _balance >= value;

        public long BalanceValue { get => _balance; set => _balance = value; }
        public long PassiveReward { get => _passiveIncome; set => _passiveIncome = value; }

        private Spawner _spawner;

        private long _passiveIncome;
        public event Action PassiveIncomeChanged;

        private void Awake()
        {
            _spawner = FindObjectOfType<Spawner>();
        }

        private void OnEnable()
        {
            OnDataChanged?.Invoke(KEY_BALANCE, StringParser.ParseFloatToShortString(_balance, 2));
            DisplayPassiveIncome();
            _spawner.ObjectChanged += DisplayPassiveIncome;
        }

        private void OnDisable()
        {
            _spawner.ObjectChanged -= DisplayPassiveIncome;
        }

        public void DisplayPassiveIncome()
        {
            var passiveReward = 0;

            foreach (var spawnedObject in _spawner.GetObjects())
            {
                passiveReward += _chain.GetStep(spawnedObject.GetComponent<Mergable>().CurrentLevel).MoneyPerSecond * PassiveRewardMultiplier;
            }

            _passiveIncome = passiveReward;
            OnDataChanged?.Invoke(KEY_PASSIVE_REWARD, StringParser.ParseFloatToShortString(_passiveIncome, 2) + "/сек");
            PassiveIncomeChanged?.Invoke();
        }

        public void IncreaseBalance(long value)
        {
            _balance += value;
            OnDataChanged?.Invoke(KEY_BALANCE, StringParser.ParseFloatToShortString(_balance, 2));
        }

        public long CalculatePassiveReward(long seconds)
        {
            return _passiveIncome * seconds;
        }

        public void DecreaseBalance(long value)
        {
            _balance -= value;
            OnDataChanged?.Invoke(KEY_BALANCE, StringParser.ParseFloatToShortString(_balance, 2));
        }
    }
}