using Spawn;
using UnityEngine;
<<<<<<< Updated upstream
using YG;
using UI.Text;
using System;
using Game;
using Evolution.Chains;

namespace Money
{
    public class ShopItem : MonoBehaviour, ITextUser
    {
        [SerializeField] private int _cost;
        [SerializeField] private int _costMultiplier;
        [SerializeField] private int _level;

        [SerializeField] private int _ygID;

=======
using UI.Text;
using System;
using Game;
using Evolution;
using UI.IconView;

namespace Money
{
    public class ShopItem : MonoBehaviour, ITextUser, IImageUser
    {
        [SerializeField] private long _cost;
        [SerializeField] private float _costMultiplier;
        [SerializeField] private int _level;

>>>>>>> Stashed changes
        [SerializeField] private EvolutionChain _chain;

        private Balance _balance;
        private Spawner _spawner;

        private int _purchasedCount = 0;

        public event Action<string, string> OnDataChanged;
<<<<<<< Updated upstream

        private const string KEY_COST = nameof(KEY_COST);
        private const string KEY_PURCHASED_COUNT = nameof(KEY_PURCHASED_COUNT);
        private const string KEY_NAME = nameof(KEY_NAME);
        private const string KEY_PASSIVE_REWARD = nameof(KEY_PASSIVE_REWARD);
=======
        public event Action<Sprite> SpriteChanged;

        private const string KEY_COST = "Cost";
        private const string KEY_PURCHASED_COUNT = "PurchasedCount";
        private const string KEY_NAME = "Name";
        private const string KEY_PASSIVE_REWARD = "PassiveReward";

        public long Cost { get => _cost; set => _cost = value; }
        public int PurchasedCount { get => _purchasedCount; set => _purchasedCount = value; }
>>>>>>> Stashed changes

        private void Awake() 
        {
            _balance = FindObjectOfType<Balance>();
            _spawner = FindObjectOfType<Spawner>();
<<<<<<< Updated upstream
        }     
=======
        }

        private void Start()
        {
            OnDataChanged?.Invoke(KEY_COST, StringParser.ParseFloatToShortString(_cost, 2));
            OnDataChanged?.Invoke(KEY_PURCHASED_COUNT, $"Куплено:{StringParser.ParseFloatToShortString(_purchasedCount, 2)}");
            OnDataChanged?.Invoke(KEY_NAME, _chain.GetStep(_level).Name);
            ChangePassiveReward();
            SpriteChanged?.Invoke(_chain.GetStep(_level).Sprite);
        }

        private void OnEnable()
        {
            OnDataChanged?.Invoke(KEY_PASSIVE_REWARD, $"{StringParser.ParseFloatToShortString(_chain.GetStep(_level).MoneyPerSecond * _balance.PassiveRewardMultiplier, 2)}/сек");
        }

        private void OnDisable()
        {
            _balance.PassiveIncomeChanged -= ChangePassiveReward;
        }

        private void ChangePassiveReward()
        {
            OnDataChanged?.Invoke(KEY_PASSIVE_REWARD, $"{StringParser.ParseFloatToShortString(_chain.GetStep(_level).MoneyPerSecond * _balance.PassiveRewardMultiplier, 2)}/сек");
        }
>>>>>>> Stashed changes

        public void TryBuy()
        {
            if (_balance.HasMoney(_cost))
            {
                _balance.DecreaseBalance(_cost);
                OnDataChanged?.Invoke(KEY_COST, StringParser.ParseFloatToShortString(_cost, 2));
<<<<<<< Updated upstream
                _spawner.SpawnObject(_level, ScreenUtils.GetRandomVisiblePosition(Camera.main, 0.5f));
                _cost *= _costMultiplier;
                _purchasedCount++;
            }
        }

        private void BuyByAd(int id)
        {
            if(id != _ygID) return;

            _spawner.SpawnObject(_level, ScreenUtils.GetRandomVisiblePosition(Camera.main, 0.5f));
            _purchasedCount++;
        }

        private void OnEnable()
        {
            YandexGame.RewardVideoEvent += BuyByAd;
            OnDataChanged?.Invoke(KEY_COST, StringParser.ParseFloatToShortString(_cost, 2));
            OnDataChanged?.Invoke(KEY_PURCHASED_COUNT, StringParser.ParseFloatToShortString(_cost, 2));
            OnDataChanged?.Invoke(KEY_NAME, _chain.GetStep(_level).Name);
            OnDataChanged?.Invoke(KEY_PASSIVE_REWARD, StringParser.ParseFloatToShortString(_chain.GetStep(_level).MoneyPerSecond * _balance.PassiveRewardMultiplier, 2));
        }

        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= BuyByAd;
        }

        public void InitializeTextView(TextView view)
        {
            
        }
=======
                _spawner.SpawnObjectWithRandomPosition(_level);
                _cost = Mathf.RoundToInt(_cost * _costMultiplier);
                _purchasedCount++;
                OnDataChanged?.Invoke(KEY_PURCHASED_COUNT, $"Куплено:{StringParser.ParseFloatToShortString(_purchasedCount, 2)}");
            }
        }    
>>>>>>> Stashed changes
    }
}