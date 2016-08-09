using UnityEngine;

namespace Assets.Scripts
{
    public class MoveToTarget : MonoBehaviour, IMovable
    {
        private Vector2 _targetPosition = Vector2.zero;
        private float _minimumDelta = 0.02f;
        public float Speed;

        void Update()
        {
            if (_targetPosition != Vector2.zero)
            {
                var vector = (_targetPosition - (Vector2)transform.position);
                if (vector.magnitude < _minimumDelta)
                {
                    _targetPosition = Vector2.zero;
                    return;
                }

                transform.position += (Vector3)(vector.normalized) * Time.deltaTime * Speed;
            }
        }

        public void MoveToPosition(Vector2 targetPosition)
        {
            _targetPosition = targetPosition;
        }
    }
}
