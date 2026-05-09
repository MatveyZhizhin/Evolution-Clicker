using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private AudioSource[] _audioSources;

        [SerializeField] private Sprite _soundOnSprite;
        [SerializeField] private Sprite _soundOffSprite;

        [SerializeField] private Image _currentImage;

        private bool _isSoundOn = true;

        public void ChangeVolume()
        {
            if (_isSoundOn)
            {
                _currentImage.sprite = _soundOffSprite;
                foreach (var source in _audioSources)
                {
                    source.volume = 0f;
                }
                _isSoundOn = false;
            }
            else
            {
                _currentImage.sprite = _soundOnSprite;
                foreach (var source in _audioSources)
                {
                    source.volume = 1f;
                }
                _isSoundOn = true;
            }
        }
    }
}
