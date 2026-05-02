using Money;
using Spawn;

namespace Upgrades
{
    public class SpawnRateUpgrader : Upgrader
    {
        private Spawner _spawner;

        private void Awake()
        {
            _balance = FindObjectOfType<Balance>();
            _spawner = FindObjectOfType<Spawner>();
        }

        protected override string GetDisplayableText() => $"{_spawner.SpawnRate}с  {_spawner.SpawnRate - _value}с";

        protected override void Upgrade()
        {
            base.Upgrade();
            _spawner.SpawnRate -= _value;
        }
    }
}