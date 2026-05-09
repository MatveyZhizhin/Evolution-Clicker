using System;
using UI.Text;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Timer))]
    public class TimerView : MonoBehaviour, ITextUser
    {
        [SerializeField] private Image _timerImage;
        private Timer _timer;

        public event Action<string, string> OnDataChanged;

        private const string KEY_TIMER = "Timer";

        [SerializeField] private bool _isDisabled;

        private void Awake()
        {
            TryGetComponent(out _timer);
        }

        private void Start()
        {
            if (_isDisabled)
                DisableTimer();
        }

        private void RenderTimer(float value, int time)
        {
            _timerImage.fillAmount = value;
            OnDataChanged?.Invoke(KEY_TIMER, time.ToString());
        }

        private void EnableTimer()
        {
            _timerImage.gameObject.SetActive(true);
        }

        private void DisableTimer()
        {
            _timerImage.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _timer.Started -= EnableTimer;
            _timer.Ended += DisableTimer;
            _timer.Updated += RenderTimer;
        }

        private void OnDisable()
        {
            _timer.Started += EnableTimer;
            _timer.Ended -= DisableTimer;
            _timer.Updated -= RenderTimer;
        }
    }
}
