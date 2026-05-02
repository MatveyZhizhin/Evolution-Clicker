using Spawn;
using Money;

namespace Upgrades
{
    public class SpawnableObjectLevelUpgrader : Upgrader
    {
        private Spawner _spawner;

        private void Awake()
        {
            _balance = FindObjectOfType<Balance>();
            _spawner = FindObjectOfType<Spawner>();
        }

        protected override string GetDisplayableText() => $"Уровень: {_spawner.StartLevel + 1}";

        protected override void Upgrade()
        {
            base.Upgrade();
            _spawner.StartLevel++;
        }
    }
} 