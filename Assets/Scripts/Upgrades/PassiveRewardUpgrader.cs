namespace Upgrades
{
    public class PassiveRewardUpgrader : Upgrader
    {
        protected override string GetDisplayableText() => $"{_balance.PassiveRewardMultiplier}X  {_balance.PassiveRewardMultiplier + _value}X";

        protected override void Upgrade()
        {
            base.Upgrade();
            _balance.PassiveRewardMultiplier += _value;
            _balance.DisplayPassiveIncome();
        }
    }
}

