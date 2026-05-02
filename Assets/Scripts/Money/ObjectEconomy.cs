using Evolution.Chains;
using Evolution;
using UnityEngine;

namespace Money
{
    [RequireComponent(typeof(Collider2D))]
    public class ObjectEconomy : MonoBehaviour
    {
        private EvolutionChain _config;
        private Balance _globalBalance;

        private Mergable _mergable;

        private void Awake()
        {
            _mergable = GetComponent<Mergable>();
        }

        private void OnMouseDown()
        {
            if (_mergable != null && !_mergable.IsAvailable) return;

            GiveClickReward();
        }

        private void GiveClickReward()
        {
            if (_config == null || _globalBalance == null) return;

            int level = _mergable != null ? _mergable.CurrentLevel : 0;
            var step = _config.GetStep(level);

            if (step != null)
            {
                _globalBalance.IncreaseBalance(step.MoneyPerClick * _globalBalance.ClickRewardMultiplier);
            }
        }

        public void StartPassiveIncome()
        {
            StopAllCoroutines();
            StartCoroutine(PassiveIncomeRoutine());
        }

        private System.Collections.IEnumerator PassiveIncomeRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                if (!gameObject.activeInHierarchy) yield break;

                if (_config == null || _globalBalance == null) continue;

                int level = _mergable != null ? _mergable.CurrentLevel : 0;
                var step = _config.GetStep(level);

                if (step != null && step.MoneyPerSecond > 0)
                {
                    _globalBalance.IncreaseBalance(step.MoneyPerSecond * _globalBalance.PassiveRewardMultiplier);
                }
            }
        }
        public void Initialize(Balance balance, EvolutionChain config)
        {
            _globalBalance = balance;
            _config = config;
            StartPassiveIncome();
        }
    }
}

