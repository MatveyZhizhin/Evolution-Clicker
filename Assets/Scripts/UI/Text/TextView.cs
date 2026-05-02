using TMPro;
using UnityEngine;

namespace UI.Text
{
    [RequireComponent(typeof(ITextUser))]
    public class TextView : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI _text;

        [Header("Settings")]
        [Tooltip("Ключ данных, которые должен отображать этот текст (например, 'Balance')")]
        [SerializeField] private string _dataKey;

        private ITextUser _textUser;

        private void Awake()
        {
            if (_textUser == null)
            {
                TryGetComponent(out _textUser);
            }
        }

        private void OnEnable()
        {
            if (_textUser != null)
            {
                _textUser.OnDataChanged += HandleDataChanged;
            }
        }

        private void OnDisable()
        {
            if (_textUser != null)
            {
                _textUser.OnDataChanged -= HandleDataChanged;
            }
        }

        private void HandleDataChanged(string key, string value)
        {
            if (key == _dataKey)
            {
                DisplayText(value);
            }
        }

        private void DisplayText(string text)
        {
            if (_text != null)
            {
                _text.text = text;
            }
        }

        public string GetDataKey() => _dataKey;
    }
}