using UnityEngine;
using Evolution;

namespace Movement
{
    [RequireComponent(typeof(Collider2D))]
    public class Draggable : MonoBehaviour
    {
        private Camera _mainCamera; 

        private Vector3 _offset;
        private bool _isDragging = false;
        private Mergable _mergable;

        public bool IsDragging => _isDragging;

        private void Awake()
        {
            _mainCamera = Camera.main;

            _mergable = GetComponent<Mergable>();
        }

        private void OnMouseDown()
        {
           
            if (_mergable != null && !_mergable.IsAvailable)
                return;

            _isDragging = true;

            Vector3 screenPoint = _mainCamera.WorldToScreenPoint(transform.position);
            _offset = transform.position - _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }

        private void OnMouseDrag()
        {
            if (!_isDragging) return;

            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mainCamera.WorldToScreenPoint(transform.position).z);
            Vector3 curPosition = _mainCamera.ScreenToWorldPoint(curScreenPoint) + _offset;

            transform.position = curPosition;
        }

        private void OnMouseUp()
        {
            if (!_isDragging) return;

            _isDragging = false;

            if (_mergable != null)
            {
                _mergable.TryMerge();
            }
        }
    }
}
