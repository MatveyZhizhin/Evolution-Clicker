using System.Collections;
using TMPro;
using UnityEngine;

namespace Tutorial
{
    [RequireComponent(typeof(TutorialHighlight))]
    public class TutorialManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private TutorialStep _firstStep;
        [SerializeField] private TextMeshProUGUI _shopText;

        private TutorialStep _currentStep;
        private TutorialHighlight _highlight;

        public bool IsTutorialCompleted { get; set; } = false;
        private void Awake()
        {
            TryGetComponent(out _highlight);
        }

        private void Start()
        {
            if (IsTutorialCompleted)
            {
                _shopText.gameObject.SetActive(false);
                return;
            }
                

            if (_firstStep != null)
                StartStep(_firstStep);
        }

        public bool GetTutorialCompletion() => IsTutorialCompleted;

        public void StartStep(TutorialStep step)
        {
            _currentStep = step;

            // 2. Подсветить объект, если есть
            if (step.targetObject != null || !string.IsNullOrEmpty(step.targetTag))
            {
                if (step.completionTrigger == TutorialStep.TriggerType.OnMerge)
                    HighlightTarget(step, true);
                else
                    HighlightTarget(step);
               
            }

            // 3. Если условие выполнения - None (автостарт следующего), запускаем таймер
            if (step.completionTrigger == TutorialStep.TriggerType.None && step.nextStep != null)
            {
                StartCoroutine(WaitAndNext(3f)); // Показывать 3 секунды
            }
        }

        private void HighlightTarget(TutorialStep step, bool isMerge = false)
        {
            Transform target = step.targetObject;

            if (target == null && !string.IsNullOrEmpty(step.targetTag))
            {
                var go = GameObject.FindGameObjectWithTag(step.targetTag);
                if (go != null) target = go.transform;
            }

            if (target != null)
            {
                if (!isMerge)
                    _highlight.SetTarget(target);
                else
                {
                    foreach (var mergable in GameObject.FindGameObjectsWithTag(step.targetTag))
                    {
                        if (mergable != target.gameObject)
                        {
                            _highlight.HighlightMergable(target, mergable.transform);
                            break;
                        }
                    }
                }                              
            }
        }

        public void CompleteStep()
        {
            if (IsTutorialCompleted)
                return;

            if (_currentStep == null) return;
   
            _highlight.StopHighlighting();

            // Переходим к следующему
            if (_currentStep.nextStep != null)
            {
                StartStep(_currentStep.nextStep);
            }
            else
            {
                FinishTutorial();
            }
        }

        private void FinishTutorial()
        {
            IsTutorialCompleted = true;
            Debug.Log("Обучение завершено!");
        }

        public void OnObjectMerged()
        {
            if (_currentStep == null || _currentStep.completionTrigger != TutorialStep.TriggerType.OnMerge) return;
            CompleteStep();
        }

        public void OnObjectClicked(GameObject clickedObj)
        {        
            if (_currentStep == null || _currentStep.completionTrigger != TutorialStep.TriggerType.OnClickObject) return;

            _shopText.gameObject.SetActive(false);

            // Проверка тега или ссылки
            if (!string.IsNullOrEmpty(_currentStep.targetTag) && clickedObj.CompareTag(_currentStep.targetTag))
            {
                CompleteStep();
            }
            else if (_currentStep.targetObject != null && clickedObj.transform == _currentStep.targetObject)
            {
                CompleteStep();
            }
        }

        public void OnObjectSpawn()
        {
            if (_currentStep == null || _currentStep.completionTrigger != TutorialStep.TriggerType.OnSpawn) return;
            CompleteStep();
        }

        private IEnumerator WaitAndNext(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            CompleteStep();
        }
    }
}
