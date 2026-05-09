using Money;
using YG;

namespace Upgrades.YGUpgrades
{
    public class ProfitDoubler : YGUpgrader
    {
        private Balance _balance;

        private void Awake()
        {
            _balance = FindObjectOfType<Balance>();
        }

        protected override void Upgrade()
        {
            _timer.StartTimer();
        }

        private void IncreaseMultiplier()
        {
            _balance.PassiveRewardMultiplier *= _value;
            _balance.ClickRewardMultiplier *= _value;
            _balance.DisplayPassiveIncome();
        }

        private void DecreaseMultiplier()
        {
            _balance.PassiveRewardMultiplier /= _value;
            _balance.ClickRewardMultiplier /= _value;
            _balance.DisplayPassiveIncome();
        }

        private void OnEnable()
        {
            _timer.Started += IncreaseMultiplier;
            _timer.Ended -= DecreaseMultiplier;
            YandexGame.RewardVideoEvent += HandleUpgrade;
        }

        private void OnDisable()
        {
            _timer.Started -= IncreaseMultiplier;
            _timer.Ended += DecreaseMultiplier;
            YandexGame.RewardVideoEvent -= HandleUpgrade;
        }
    }
}
