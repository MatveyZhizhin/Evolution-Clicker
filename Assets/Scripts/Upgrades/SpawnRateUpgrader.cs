using Money;
using Spawn;
using System;
using UnityEngine;

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
            if (_spawner.SpawnRate - _value <= 0)
                return;

            base.Upgrade();
            _spawner.SpawnRate -= _value;
        }
    }
}