using Money;
using Spawn;
using YG;
using Evolution;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using Upgrades;
using Game.Skins;

namespace Game
{
    public class SaveManager : MonoBehaviour
    {
        private Balance _balance;
        private Spawner _spawner;

        [SerializeField] private Upgrader[] _upgraders;
        [SerializeField] private ShopItem[] _shopItems;
        [SerializeField] private Skin[] _skins;

        private void Awake()
        {
            _balance = FindObjectOfType<Balance>();
            _spawner = FindObjectOfType<Spawner>();
        }

        private void Save()
        {
            YandexGame.savesData.Seconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            YandexGame.savesData.Balance = _balance.BalanceValue;

            YandexGame.savesData.ClickRewardMultiplier = _balance.ClickRewardMultiplier;
            YandexGame.savesData.PassiveRewardMultiplier = _balance.PassiveRewardMultiplier;

            for (int i = 0; i < _upgraders.Length; i++)
            {
                YandexGame.savesData.UpgradeLevel[i] = _upgraders[i].Level;
                YandexGame.savesData.UpgradeCost[i] = _upgraders[i].Cost;
            }

            for (int i = 0; i < _shopItems.Length; i++)
            {
                YandexGame.savesData.ShopCost[i] = _shopItems[i].Cost;
                YandexGame.savesData.PurchasedCount[i] = _shopItems[i].PurchasedCount;
            }

            for (int i = 0; i < _skins.Length; i++)
            {
                YandexGame.savesData.IsBought[i] = _skins[i].IsBought;
                YandexGame.savesData.IsSelected[i] = _skins[i].IsSelected;
            }

            SaveObjects();

            YandexGame.SaveProgress();
        }

        private void SaveObjects()
        {
            var objects = _spawner.GetObjects();

            for (int i = 0; i < objects.Length; i++)
            {
                YandexGame.savesData.X[i] = objects[i].transform.position.x;
                YandexGame.savesData.Y[i] = objects[i].transform.position.y;

                YandexGame.savesData.Level[i] = objects[i].GetComponent<Mergable>().CurrentLevel;
            }

            YandexGame.savesData.Count = objects.Length;
        }

        private void Load()
        {
            _balance.BalanceValue = YandexGame.savesData.Balance;
            _balance.ClickRewardMultiplier = YandexGame.savesData.ClickRewardMultiplier;
            _balance.PassiveRewardMultiplier = YandexGame.savesData.PassiveRewardMultiplier;

            for (int i = 0; i < _upgraders.Length; i++)
            {
                _upgraders[i].Level = YandexGame.savesData.UpgradeLevel[i];
                _upgraders[i].Cost = YandexGame.savesData.UpgradeCost[i];
            }

            for (int i = 0; i < _shopItems.Length; i++)
            {
                 _shopItems[i].Cost = YandexGame.savesData.ShopCost[i];
                 _shopItems[i].PurchasedCount = YandexGame.savesData.PurchasedCount[i];
            }

            for (int i = 0; i < _skins.Length; i++)
            {
                _skins[i].IsBought = YandexGame.savesData.IsBought[i];
                _skins[i].IsSelected = YandexGame.savesData.IsSelected[i];
            }

            for (int i = 0; i < YandexGame.savesData.Count; i++)
            {
                _spawner.SpawnObject(YandexGame.savesData.Level[i],new Vector2(YandexGame.savesData.X[i], YandexGame.savesData.Y[i]));
            }
        }

        private void ResetProgress()
        {
            YandexGame.ResetSaveProgress();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetProgress();
            }
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        private void OnEnable()
        {
            YandexGame.GetDataEvent += Load;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= Load;
        }
    }
}
