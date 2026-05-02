using Evolution;
using Spawn;
using System.Collections;
using UI;
using UnityEngine;

namespace Movement
{
    [RequireComponent(typeof(Mergable))]
    public class RandomMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private float _minWaitTime = 1f;
        [SerializeField] private float _maxWaitTime = 3f;

        private Camera _mainCamera;

        private Vector3 _targetPosition;
        private Coroutine _wanderCoroutine;
        private Mergable _mergable;
        private Draggable _draggable;

        private void Awake()
        {
            if (_mainCamera == null) _mainCamera = Camera.main;

            _mergable = GetComponent<Mergable>();
            _draggable = GetComponent<Draggable>();

            Invoke(nameof(StartWandering), Random.Range(0f, _maxWaitTime));
        }

        private void OnDestroy()
        {
            StopWandering();
        }


        public void StartWandering()
        {
            if (_wanderCoroutine != null) StopCoroutine(_wanderCoroutine);
            _wanderCoroutine = StartCoroutine(WanderRoutine());
        }

        public void StopWandering()
        {
            if (_wanderCoroutine != null)
            {
                StopCoroutine(_wanderCoroutine);
                _wanderCoroutine = null;
            }
        }

        private IEnumerator WanderRoutine()
        {
            while (true)
            {
                while (!CanMove())
                {
                    yield return null;
                }

                yield return new WaitForSeconds(Random.Range(_minWaitTime, _maxWaitTime));

                if (!CanMove()) continue;

                PickNewTarget();

                while (CanMove() && Vector3.Distance(transform.position, _targetPosition) > 0.05f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
                    yield return null;
                }
            }
        }

        private bool CanMove()
        {
            bool isMerging = _mergable != null && !_mergable.IsAvailable;
            bool isDragging = _draggable != null && _draggable.IsDragging;
            return !isMerging && !isDragging;
        }

        private void PickNewTarget()
        {
            _targetPosition = ScreenUtils.GetRandomVisiblePosition(Camera.main, 0.5f);
            _targetPosition.z = transform.position.z;
        }
    }
}
