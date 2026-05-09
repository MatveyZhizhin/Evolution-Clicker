using System.Collections;
using TMPro;
using UnityEngine;

namespace Floating
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private float _fadeDuration = 0.5f;
        [SerializeField] private Vector3 _direction = new Vector3(0, 1, 0);

        private TextMeshProUGUI _textMesh;
        private Color _startColor;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
            if (_textMesh == null)
                _textMesh = GetComponentInChildren<TextMeshProUGUI>();

            _startColor = _textMesh.color;
        }

        public void Initialize(string text, Vector3 position)
        {
            transform.position = position;
            _textMesh.text = text;
            _textMesh.color = _startColor;

            StartCoroutine(AnimateAndDestroy());
        }

        private IEnumerator AnimateAndDestroy()
        {
            float elapsed = 0f;
            Vector3 startPos = transform.position;

            while (elapsed < _fadeDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / _fadeDuration;

                transform.position = startPos + (_direction * _moveSpeed * t);

                Color c = _textMesh.color;
                c.a = Mathf.Lerp(1, 0, t);
                _textMesh.color = c;

                yield return null;
            }

            Destroy(gameObject);
        }
    }
}
