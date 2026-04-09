using Evolution.Chains;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evolution
{
    public static class Merger
    {
        public static void StartMerge(Mergable item1, Mergable item2, EvolutionChain config)
        {
            item1.SetLocked(true);
            item2.SetLocked(true);

            item1.StartCoroutine(MergeRoutine(item1, item2, config));
        }

        private static IEnumerator MergeRoutine(Mergable item1, Mergable item2, EvolutionChain config)
        {
            Transform t1 = item1.transform;
            Transform t2 = item2.transform;

            int level = item1.CurrentLevel;
            var step = config.GetStep(level);

            Vector3 targetPos = (t1.position + t2.position) / 2f;

            float distance = Vector3.Distance(t1.position, targetPos);
            float duration = distance / config.MergeSpeed;

            if (duration < 0.05f) duration = 0.05f;

            Vector3 start1 = t1.position;
            Vector3 start2 = t2.position;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);

                float easeT = t * t;

                t1.position = Vector3.Lerp(start1, targetPos, easeT);
                t2.position = Vector3.Lerp(start2, targetPos, easeT);

                yield return null;
            }

            t1.position = targetPos;
            t2.position = targetPos;

            SpawnNextLevel(targetPos, level, config);

            Object.Destroy(item1.gameObject);
            Object.Destroy(item2.gameObject);
        }

        private static void SpawnNextLevel(Vector3 position, int currentLevel, EvolutionChain config)
        {
            int nextLevel = currentLevel + 1;

            if (nextLevel <= config.MaxLevel)
            {
                var nextStep = config.GetStep(nextLevel);

                if (nextStep != null && config.Prefab != null)
                {
                    GameObject newObj = Object.Instantiate(config.Prefab, position, Quaternion.identity);
                    newObj.GetComponent<SpriteRenderer>().sprite = nextStep;

                    Mergable newMergable = newObj.GetComponent<Mergable>();
                }
            }
        }
    }
}
