using UnityEngine;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "New Tutorial Step", menuName = "Tutorial/TutorialStep")]
    public class TutorialStep : ScriptableObject
    {
        public enum TriggerType
        {
            None,
            OnClickObject,
            OnMerge,
            OnSpawn,
        }

        [Header("Target")]
        public Transform targetObject;
        public string targetTag;

        [Header("Completion Condition")]
        public TriggerType completionTrigger;

        [Header("Next Step")]
        public TutorialStep nextStep;
    }
}