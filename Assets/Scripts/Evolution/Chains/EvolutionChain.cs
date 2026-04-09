using UnityEngine;

namespace Evolution.Chains
{
    [CreateAssetMenu(fileName = "EvolutionChain", menuName = "Evolution/EvolutionChain")]
    public class EvolutionChain : ScriptableObject
    {
        public float MergeRadius;
        public float MergeSpeed;

        public GameObject Prefab;

        [System.Serializable]
        public class EvolutionStep
        {
            public Sprite Sprite;
        }

        [SerializeField] private EvolutionStep[] _steps;

        public Sprite GetStep(int level)
        {
            if (level >= 0 && level < _steps.Length)
                return _steps[level].Sprite;

            return null;
        }

        public int GetLevel(Sprite sprite)
        {
            for (int i = 0; i < _steps.Length; i++)
            {
                if (sprite == _steps[i].Sprite)
                    return i;
            }

            return 0;
        }

        public int MaxLevel => _steps.Length - 1;
    }
}