using UnityEngine;
using UnityEngine.UI;

namespace UI.IconView
{
    [RequireComponent(typeof(IImageUser))]
    public class ImageView : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        private IImageUser _imageUser;

        private void Awake()
        {
            TryGetComponent(out _imageUser);
        }

        private void ChangeSprite(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        private void OnEnable()
        {
            _imageUser.SpriteChanged += ChangeSprite;
        }

        private void OnDisable()
        {
            _imageUser.SpriteChanged -= ChangeSprite;
        }
    }
}
