namespace Upgrades
{
    public class ClickRewardUpgrader : Upgrader
    {
        protected override string GetDisplayableText() => $"{_balance.ClickRewardMultiplier}X  {_balance.ClickRewardMultiplier + _value}X";


        protected override void Upgrade()
        {
            base.Upgrade();
            _balance.ClickRewardMultiplier += _value;
        }

    }
}

