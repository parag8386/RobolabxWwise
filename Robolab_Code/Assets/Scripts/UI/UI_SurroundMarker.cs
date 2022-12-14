namespace Robolab.UI
{
    using UnityEngine;

    public class UI_SurroundMarker : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _marker = default;

        [SerializeField] 
        private Camera _UICamera;

        [SerializeField] 
        private Canvas _canvas;

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

            Vector3 screenPosition = _camera.WorldToScreenPoint(_target.position);
            if (screenPosition.z < 0f)
            {
                _marker.localPosition = new Vector3(Screen.currentResolution.width * 2, Screen.currentResolution.height * 2, 0f);
                return;
            }
            screenPosition.z = (_canvas.transform.position - _UICamera.transform.position).magnitude;

            Vector3 finalPosition = _UICamera.ScreenToWorldPoint(screenPosition);
            _marker.position = finalPosition;
        }
    }
}