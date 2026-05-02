
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
        public long Balance = 0;
        public int PassiveRewardMultiplier = 1;
        public int ClickRewardMultiplier = 1;

        public long[] UpgradeCost = new long[4];
        public int[] UpgradeLevel = new int[4];

        public long[] ShopCost = new long[8];
        public int[] PurchasedCount = new int[8];

        public int Count = 0;
        public int[] Level = new int[12];
        public float[] X = new float[12];
        public float[] Y = new float[12];

        public bool[] IsSelected = new bool[4];
        public bool[] IsBought = new bool[4];


        // ...

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;
        }
    }
}
