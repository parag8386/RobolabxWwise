namespace Robolab.Story.Behaviour
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Story_CameraShake : StoryBehaviourBase
    {
        [SerializeField]
        private float _minTimeForCameraShake, _maxTimeForCameraShake;

        [SerializeField]
        private float _minCooldownForCameraShake, _maxCooldownForCameraShake;

        [SerializeField]
        private float _shakeIntensity = 0.7f, _decreaseFactor = 1f;

        private Transform _cameraTransform;

        private Vector3 _originalCameraPos;

        private float _timeToNextCameraShake;
        private float _cameraShakeTime;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _cameraTransform = Camera.main.transform;
            _originalCameraPos = _cameraTransform.localPosition;

            GenerateRandomTimes();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_timeToNextCameraShake <= 0f)
            {
                ShakeCamera();
            }
            else
            {
                _timeToNextCameraShake -= Time.deltaTime;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _timeToNextCameraShake = 0f;
            _cameraShakeTime = 0f;
            _cameraTransform.localPosition = _originalCameraPos;
        }

        private void ShakeCamera()
        {
            if (_cameraShakeTime > 0f)
            {
                _cameraTransform.localPosition = _originalCameraPos + Random.insideUnitSphere * _shakeIntensity;
                _cameraShakeTime -= Time.deltaTime * _decreaseFactor;
            }
            else
            {
                _cameraShakeTime = 0f;
                _cameraTransform.localPosition = _originalCameraPos;
                GenerateRandomTimes();
            }
        }

        private void GenerateRandomTimes()
        {
            _timeToNextCameraShake = Random.Range(minInclusive: _minCooldownForCameraShake, maxInclusive: _maxCooldownForCameraShake);
            _cameraShakeTime = Random.Range(minInclusive: _minTimeForCameraShake, maxInclusive: _maxTimeForCameraShake);
        }
    }
}