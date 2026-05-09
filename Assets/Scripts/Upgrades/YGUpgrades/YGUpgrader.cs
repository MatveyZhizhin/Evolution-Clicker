using UI;
using UnityEngine;
using YG;

namespace Upgrades.YGUpgrades
{
    public abstract class YGUpgrader : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] protected int _value;
        [SerializeField] protected Timer _timer;
        protected void HandleUpgrade(int id)
        {
            if (_id != id) return;

            Upgrade();
        }

        protected abstract void Upgrade();

        private void OnEnable()
        {
            YandexGame.RewardVideoEvent += HandleUpgrade;
        }

        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= HandleUpgrade;
        }
    }
}
