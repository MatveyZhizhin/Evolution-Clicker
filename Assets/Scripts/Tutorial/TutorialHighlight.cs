using UnityEngine;

namespace Tutorial
{
    public class TutorialHighlight : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private bool _isMoving;
        [SerializeField] private GameObject _arrow;
        [SerializeField] private Vector2 _offset;
        [SerializeField] private float _speed;

        private Transform _moveTarget;
        private Transform _currentTarget;

        public void SetTarget(Transform target)
        {
            _arrow.SetActive(true);
            _arrow.transform.SetParent(target);
            _arrow.transform.position = (Vector2)target.position + _offset;
        }

        public void HighlightMergable(Transform target1, Transform target2)
        {
            SetTarget(target1);
            _arrow.transform.SetParent(_canvas.transform);
            _moveTarget = target2;
            _currentTarget = target1;
            _isMoving = true;           
        }

        private void Update()
        {
            if (_isMoving)
            {
                MoveArrow();
            }              
        }

        private void MoveArrow()
        {
            _arrow.transform.position = Vector2.MoveTowards(_arrow.transform.position, _moveTarget.position, _speed * Time.deltaTime);
            if (_arrow.transform.position == _moveTarget.position)
                _arrow.transform.position = _currentTarget.position;
        }

        public void StopHighlighting()
        {
            _isMoving = false;
            _arrow.transform.SetParent(_canvas.transform);
            _arrow.SetActive(false);
        }
    }
}
