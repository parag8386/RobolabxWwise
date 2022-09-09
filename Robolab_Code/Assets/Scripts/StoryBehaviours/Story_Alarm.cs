namespace Robolab.Story.Behaviour
{
    using UnityEngine;

    public class Story_Alarm : Story_PlaySound
    {
        private const string ALARM_STATE_INDEX_PARAMETER = "Alarm_State_Index";

        [SerializeField] private Color _alarmColorForLights = Color.red;

        [SerializeField] private float _defaultLightIntensity = 1f;
        [SerializeField] private float _alarmPulseTime = 1f;
        [SerializeField] private float _alarmDecayMultiplier = 1f;

        private float _lastAlarmPulseTime = 0f;
        private float _lightIntensity = 1f;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _storyStateIndexParameterHash = Animator.StringToHash(ALARM_STATE_INDEX_PARAMETER);

            base.OnStateEnter(animator, stateInfo, layerIndex);

            for (int i = 0; i < _storyGameObjectReferences.LightObjects.Length; i++)
            {
                _storyGameObjectReferences.LightObjects[i].color = _alarmColorForLights;
                _storyGameObjectReferences.LightObjects[i].intensity = _defaultLightIntensity;
            }

            // Turn on all lights
            for (int i = 0; i < _storyGameObjectReferences.LightObjects.Length; i++)
            {
                _storyGameObjectReferences.LightObjects[i].enabled = true;
            }

            _lightIntensity = _defaultLightIntensity;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            for (int i = 0; i < _storyGameObjectReferences.LightObjects.Length; i++)
            {
                _storyGameObjectReferences.LightObjects[i].intensity = _lightIntensity;
            }
            _lightIntensity -= (Time.deltaTime * _alarmDecayMultiplier);
            _lightIntensity = Mathf.Clamp(_lightIntensity, 0f, _defaultLightIntensity);

            if (_lastAlarmPulseTime <= 0f)
            {
                _lastAlarmPulseTime = _alarmPulseTime;
                _lightIntensity = _defaultLightIntensity;
            }
            _lastAlarmPulseTime -= Time.deltaTime;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
        }
    }
}
