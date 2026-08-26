
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения

        public long Seconds = 0;
        public long Balance = 3000;
        public int PassiveRewardMultiplier = 1;
        public int ClickRewardMultiplier = 1;
        public float SpawnRate = 30;
        public int StartLevel = 0;

        public long[] UpgradeCost = new long[4];
        public int[] UpgradeLevel = new int[4];

        public long[] ShopCost = new long[19];
        public int[] PurchasedCount = new int[19];

        public int Count = 0;
        public int[] Level = new int[12];
        public float[] X = new float[12];
        public float[] Y = new float[12];

        public bool[] IsSelected = new bool[4];
        public bool[] IsBought = new bool[4];

        public bool IsTutorialCompleted = false;
        public int CurrentStepIndex = 0;

        // ...

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;

            UpgradeCost[0] = 30000;
            UpgradeCost[1] = 1000000;
            UpgradeCost[2] = 12000;
            UpgradeCost[3] = 10000;
            ShopCost[0] = 800;
            ShopCost[1] = 1500;
            ShopCost[2] = 4000;
            ShopCost[3] = 12000;
            ShopCost[4] = 34000;
            ShopCost[5] = 96000;
            ShopCost[6] = 270000;
            ShopCost[7] = 700000;
            ShopCost[8] = 1974000;
            ShopCost[9] = 5566680;
            ShopCost[10] = 15698037;
            ShopCost[11] = 44268466;
            ShopCost[12] = 352040549;
            ShopCost[13] = 992754348;
            ShopCost[14] = 2799567264;
            ShopCost[15] = 7894779684;
            ShopCost[16] = 22263278710;
            ShopCost[17] = 62782445963;
            ShopCost[18] = 177046497617;
        }
    }
}
