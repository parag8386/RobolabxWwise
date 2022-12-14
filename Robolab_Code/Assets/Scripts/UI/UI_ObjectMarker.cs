namespace Robolab.UI
{
    using UnityEngine;

    public class UI_ObjectMarker : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _objectMarker = default;

        [SerializeField]
        private GameObject _leftArrowObject, _rightArrowObject = default;

        [SerializeField]
        private Transform _playerTransform = default;

        [SerializeField]
        private float _scrollMultiplier = default;

        private Transform _objectToFollow = default;

        public void SetMarkerTarget(Transform target)
        {
            _objectToFollow = target;
            _objectMarker.gameObject.SetActive(target != null);

            _leftArrowObject.SetActive(target != null);
            _rightArrowObject.SetActive(target != null);
        }

        private void Awake()
        {
            SetMarkerTarget(null);
        }

        private void Update()
        {
            if (_objectToFollow == null)
            {
                return;
            }

            Vector3 playerObjectVector = (_objectToFollow.position - _playerTransform.position).normalized;
            Vector3 playerForward = _playerTransform.forward.normalized;

            float angle = Vector3.SignedAngle(playerForward, playerObjectVector, Vector3.up);
            _objectMarker.localPosition = new Vector3(angle * _scrollMultiplier, _objectMarker.localPosition.y, _objectMarker.localPosition.z);

            _leftArrowObject.SetActive(angle < -45f);
            _rightArrowObject.SetActive(angle > 45f);
                
        }
    }
}