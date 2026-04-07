using UnityEngine;

namespace Evolution
{
    [CreateAssetMenu(fileName = "EvolutionChain", menuName = "Evolution/EvolutionChain")]
    public class EvolutionChain : ScriptableObject
    {
        [System.Serializable]
        public class EvolutionStep
        {
            public int level;
            public GameObject prefab;
            public Color debugColor;  
        }

        public EvolutionStep[] steps;

        public GameObject GetNextPrefab(int currentLevel)
        {
            if (currentLevel < 0 || currentLevel >= steps.Length - 1)
                return null; 

            return steps[currentLevel + 1].prefab;
        }

        public GameObject GetPrefab(int level)
        {
            if (level < 0 || level >= steps.Length)
                return null;

            return steps[level].prefab;
        }

        public int MaxLevel => steps.Length - 1;
    }
}