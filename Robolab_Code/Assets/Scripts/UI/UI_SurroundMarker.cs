namespace Robolab.UI
{
    using UnityEngine;

    public class UI_SurroundMarker : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _marker = default;
        
        private Transform _target = default;
        
        private Camera _camera = default;

        public void SetTarget(Transform target)
        {
            _target = target;
            _marker.gameObject.SetActive(_target != null);
        }

        private void Awake()
        {
            _camera = Camera.main;
            SetTarget(null);
        }

        private void Update()
        {
            if (_target == null)
            {
                return;
            }

            Vector3 screenPosition = _camera.WorldToScreenPoint(new Vector3(_target.position.x, _target.position.y, _target.position.z));

            if (screenPosition.z < 0f)
            {
                _marker.localPosition = new Vector3(Screen.currentResolution.width * 2, Screen.currentResolution.height * 2, 0f);
                return;
            }

            screenPosition.x -= (Screen.currentResolution.width) - (screenPosition.z + _marker.sizeDelta.x);
            screenPosition.y -= (Screen.currentResolution.height / 4f) + (screenPosition.z + (_marker.sizeDelta.y / 2f));
            _marker.localPosition = screenPosition;
        }
    }
}