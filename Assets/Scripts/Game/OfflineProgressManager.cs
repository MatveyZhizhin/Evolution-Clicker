using Money;
using Spawn;
using System;
using UnityEngine;
using YG;

namespace Game
{
    public class OfflineProgressManager : MonoBehaviour
    {

        [SerializeField] private int _maxOfflineHours = 24;

        private Balance _balance;
        private Spawner _spawner;

        private void Awake()
        {
            _balance = FindObjectOfType<Balance>();
            _spawner = FindObjectOfType<Spawner>();
        }

        private void Start()
        {
            HandleOfflineProgress();
        }

        private void HandleOfflineProgress()
        {
            if (YandexGame.savesData.Seconds == 0)
                return;

            var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var differentTime = currentTime - YandexGame.savesData.Seconds;

            if (differentTime > _maxOfflineHours * 3600)
            {
                _balance.IncreaseBalance(_balance.CalculatePassiveReward(_maxOfflineHours * 3600));
            }
            else
            {
                _balance.IncreaseBalance(_balance.CalculatePassiveReward(differentTime));
            }

            int objectsAmount = (int)(differentTime / _spawner.SpawnRate);


            _spawner.SpawnObjectWithRandomPosition(_spawner.StartLevel, objectsAmount);
        }
    }
}
