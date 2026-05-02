using System;
using UI.Text;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ProgressBar
{
    [RequireComponent(typeof(IProgressBarUser))]
    public class ProgressBarView : MonoBehaviour
    {
        private IProgressBarUser _progressBarUser;

        [SerializeField] private Image _slider;


        private void Awake()
        {
            TryGetComponent(out _progressBarUser);
        }

        private void DisplayData(int value, int maxValue)
        {
            _slider.fillAmount = (float)value / maxValue;
        }

        private void OnEnable()
        {
            _progressBarUser.OnValueChanged += DisplayData;
        }

        private void OnDisable()
        {
            _progressBarUser.OnValueChanged -= DisplayData;
        }
    }
}