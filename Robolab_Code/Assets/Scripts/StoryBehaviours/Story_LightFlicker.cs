namespace Robolab.Story.Behaviour
{
    using Robolab.Wwise.Events;
    using UnityEngine;
    using UnityEngine.Animations;

    public class Story_LightFlicker : StoryBehaviourBase
    {
        private const float SECONDS_TO_STOP_FLICKER_BEFORE_EXIT = 0.2f;

        [SerializeField] private Vector2 _flickerFrequency = Vector2.zero;

        private float _flickerUpdateDuration = 0f;
        private bool _stopAll = false;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            _stopAll = false;
            _flickerUpdateDuration = Random.Range(minInclusive: _flickerFrequency.x, maxInclusive: _flickerFrequency.y);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            if (_timeInState >= SECONDS_TO_STOP_FLICKER_BEFORE_EXIT)
            {
                _flickerUpdateDuration -= Time.deltaTime;
                if (_flickerUpdateDuration <= 0f)
                {
                    for (int i = 0; i < _storyGameObjectReferences.LightObjects.Length; i++)
                    {
                        int enable = Random.Range(minInclusive: 0, maxExclusive: 2);
                        _storyGameObjectReferences.LightObjects[i].enabled = (enable > 0);
                    }
                    _flickerUpdateDuration = Random.Range(minInclusive: _flickerFrequency.x, maxInclusive: _flickerFrequency.y);
                    WwiseEventHelper.PostEventID(WwiseEventIDs.LIGHT_FLICKER, _storyGameObjectReferences.LightAudioEmitter1);
                    WwiseEventHelper.PostEventID(WwiseEventIDs.LIGHT_FLICKER, _storyGameObjectReferences.LightAudioEmitter2);
                    WwiseEventHelper.PostEventID(WwiseEventIDs.LIGHT_FLICKER, _storyGameObjectReferences.LightAudioEmitter3);
                }
            }
            else
            {
                if (!_stopAll)
                {
                    WwiseEventHelper.PostEventID(WwiseEventIDs.STOP_ALL, _storyGameObjectReferences.GenericAmbience);
                    WwiseEventHelper.PostEventID(WwiseEventIDs.STOP_MUSIC_LOOP, _storyGameObjectReferences.RetroTelevision);
                }
                _stopAll = true;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            WwiseEventHelper.StopEventID(WwiseEventIDs.LIGHT_FLICKER, _storyGameObjectReferences.LightAudioEmitter1);
            WwiseEventHelper.StopEventID(WwiseEventIDs.LIGHT_FLICKER, _storyGameObjectReferences.LightAudioEmitter2);
            WwiseEventHelper.StopEventID(WwiseEventIDs.LIGHT_FLICKER, _storyGameObjectReferences.LightAudioEmitter3);
        }
    }
}
