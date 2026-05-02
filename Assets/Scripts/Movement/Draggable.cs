<<<<<<< Updated upstream
using UnityEngine;
using Evolution;
=======
using Evolution;
using UnityEngine;
using UI;
>>>>>>> Stashed changes

namespace Movement
{
    [RequireComponent(typeof(Collider2D))]
    public class Draggable : MonoBehaviour
    {
        private Camera _mainCamera; 

        private Vector3 _offset;
        private bool _isDragging = false;
        private Mergable _mergable;

<<<<<<< Updated upstream
=======
        [SerializeField] private bool _ignoreIfEmpty = true;
        private RectTransform[] _blockedUIZones;

>>>>>>> Stashed changes
        public bool IsDragging => _isDragging;

        private void Awake()
        {
            _mainCamera = Camera.main;
<<<<<<< Updated upstream
=======
            _blockedUIZones = FindObjectOfType<UIBlockingZones>().GetZones();
>>>>>>> Stashed changes

            _mergable = GetComponent<Mergable>();
        }

        private void OnMouseDown()
        {
<<<<<<< Updated upstream
=======
            if (IsPointerOverBlockedUI()) return;
>>>>>>> Stashed changes
           
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
<<<<<<< Updated upstream
=======

        private bool IsPointerOverBlockedUI()
        {

            if (_ignoreIfEmpty && (_blockedUIZones == null || _blockedUIZones.Length == 0))
                return false;

            Vector2 mousePos = Input.mousePosition;

            foreach (var zone in _blockedUIZones)
            {
                if (zone == null) continue;
                if(!zone.gameObject.activeInHierarchy) continue;


                if (RectTransformUtility.RectangleContainsScreenPoint(zone, mousePos, _mainCamera))
                {
                    return true;
                }
            }

            return false;
        }
>>>>>>> Stashed changes
    }
}
