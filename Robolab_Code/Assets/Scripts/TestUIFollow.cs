using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIFollow : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Camera _UICamera;

    [SerializeField] private RectTransform _rectTransform;

    [SerializeField] private Transform _target;

    [SerializeField] private Canvas _canvas;

    void Update()
    {
        Vector3 pos = _camera.WorldToScreenPoint(new Vector3(_target.position.x, _target.position.y, _target.position.z));
        //pos.x -= (Screen.currentResolution.width / 2f);// - (screenPosition.z + _marker.sizeDelta.x);
        //pos.y -= (Screen.currentResolution.height / 2f);// + (screenPosition.z + (_marker.sizeDelta.y / 2f));
        pos.z = (_canvas.transform.position - _UICamera.transform.position).magnitude;
        var finalPos = _UICamera.ScreenToWorldPoint(pos);
        _rectTransform.position = finalPos;
        //float scaleFactor = _canvas.scaleFactor;
        //Vector2 finalPosition = new Vector2((pos.x / scaleFactor), pos.y / scaleFactor);
        //_rectTransform.anchoredPosition = finalPosition;
    }
}
