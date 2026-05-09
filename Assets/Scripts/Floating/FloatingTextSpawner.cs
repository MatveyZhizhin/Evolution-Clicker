using UnityEngine;

namespace Floating
{
    public class FloatingTextSpawner : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private FloatingText _textPrefab;

        [Header("Offset")]
        [Tooltip("Смещение текста относительно точки клика")]
        [SerializeField] private Vector3 _offset = new Vector3(0, 1, 0);


        public void Show(string text, Vector3 worldPosition)
        {

            Vector3 randomOffset = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(0f, 0.5f), 0);
            Vector3 spawnPos = worldPosition + _offset + randomOffset;

            FloatingText newText = Instantiate(_textPrefab, spawnPos, Quaternion.identity);
            newText.Initialize(text, spawnPos);
        }
    }
}
