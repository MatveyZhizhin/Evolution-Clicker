using Spawn;
using UnityEngine;
using UI.Text;
using System;
using Game;
using Evolution;
using UI.IconView;
using UnityEngine.Events;

namespace Money
{
    public class ShopItem : MonoBehaviour, ITextUser, IImageUser
    {
        [SerializeField] private long _cost;
        [SerializeField] private float _costMultiplier;
        [SerializeField] private int _level;

        [SerializeField] private EvolutionChain _chain;

        private Balance _balance;
        private Spawner _spawner;

        private int _purchasedCount = 0;

        public event Action<string, string> OnDataChanged;
        public event Action<Sprite> SpriteChanged;

        private const string KEY_COST = "Cost";
        private const string KEY_PURCHASED_COUNT = "PurchasedCount";
        private const string KEY_NAME = "Name";
        private const string KEY_PASSIVE_REWARD = "PassiveReward";

        public long Cost { get => _cost; set => _cost = value; }
        public int PurchasedCount { get => _purchasedCount; set => _purchasedCount = value; }

        [SerializeField] private UnityEvent _onLackOfSpace;

        private void Awake() 
        {
            _balance = FindObjectOfType<Balance>();
            _spawner = FindObjectOfType<Spawner>();
        }

        private void Start()
        {
            OnDataChanged?.Invoke(KEY_COST, StringParser.ParseFloatToShortString(_cost, 1));
            OnDataChanged?.Invoke(KEY_PURCHASED_COUNT, $"Куплено:{StringParser.ParseFloatToShortString(_purchasedCount, 1)}");
            OnDataChanged?.Invoke(KEY_NAME, _chain.GetStep(_level).Name);
            ChangePassiveReward();
            SpriteChanged?.Invoke(_chain.GetStep(_level).Sprite);
        }

        private void OnEnable()
        {
            OnDataChanged?.Invoke(KEY_PASSIVE_REWARD, $"{StringParser.ParseFloatToShortString(_chain.GetStep(_level).MoneyPerSecond * _balance.PassiveRewardMultiplier, 1)}/сек");
        }

        private void OnDisable()
        {
            _balance.PassiveIncomeChanged -= ChangePassiveReward;
        }

        private void ChangePassiveReward()
        {
            OnDataChanged?.Invoke(KEY_PASSIVE_REWARD, $"{StringParser.ParseFloatToShortString(_chain.GetStep(_level).MoneyPerSecond * _balance.PassiveRewardMultiplier, 2)}/сек");
        }

        public void TryBuy()
        {
            if (!_spawner.HasSpaceToSpawn())
            {
                _onLackOfSpace?.Invoke();
                return;
            }

            if (_balance.HasMoney(_cost))
            {
                _balance.DecreaseBalance(_cost);
                _spawner.SpawnObjectWithRandomPosition(_level);
                _cost = Mathf.RoundToInt(_cost * _costMultiplier);
                OnDataChanged?.Invoke(KEY_COST, StringParser.ParseFloatToShortString(_cost, 1));
                _purchasedCount++;
                OnDataChanged?.Invoke(KEY_PURCHASED_COUNT, $"Куплено:{StringParser.ParseFloatToShortString(_purchasedCount, 1)}");
            }
        }    
    }
}