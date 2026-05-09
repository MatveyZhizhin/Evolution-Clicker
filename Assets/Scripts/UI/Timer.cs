using System;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float _startTime;
        private float _time;

        private bool _isStarted;

        public event Action Started;
        public event Action<float, int> Updated;
        public event Action Ended;

        [SerializeField] private UnityEvent _onStart;
        [SerializeField] private UnityEvent _onEnd;

        private void Start()
        {
            _time = _startTime;
        }

        private void Update()
        {
            if (_isStarted)
            {
                if (_time <= 0)
                {
                    EndTimer();
                    _time = _startTime;
                }
                else
                {
                    _time -= Time.deltaTime;
                    Updated?.Invoke(_time / _startTime, (int)_time);
                }
            }
        }

        public void StartTimer(float time)
        {
            _startTime = time;
            Started?.Invoke();
            _onStart?.Invoke();
            _isStarted = true;
        }

        public void StartTimer()
        {
            Started?.Invoke();
            _onStart?.Invoke();
            _isStarted = true;
        }

        public void EndTimer()
        {
            Ended?.Invoke();
            _onEnd?.Invoke();
            _isStarted = false;
        }
    }
}
