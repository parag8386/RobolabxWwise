namespace Robolab.Story.Behaviour
{
    using Robolab.Wwise.Events;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Story_Breathing : StoryBehaviourBase
    {
        [SerializeField, Range(1, 100)]
        private float _breathingIntensity = 50f;

        [SerializeField]
        private float _timeToHitIntensity = 0f;

        private float _elapsedTime = 0f;

        private float _initialBreathingIntensity = 0f;
        private float _currentBreathingIntensity = 0f;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            // Post breathing event
            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_PLAYER_BREATHING, _storyGameObjectReferences.RigidbodyFirstPersonController.gameObject);

            // Get RTPC value
            WwiseEventHelper.GetRTPCValue(WwiseEventIDs.PLAYER_BREATHING_SPEED, out _initialBreathingIntensity);

            if (_timeToHitIntensity <= 0f)
            {
                WwiseEventHelper.SetRTPCValue(WwiseEventIDs.PLAYER_BREATHING_SPEED, _breathingIntensity);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            // Update RTPC value
            if (_timeToHitIntensity > 0f)
            {
                _currentBreathingIntensity = Mathf.Lerp(_initialBreathingIntensity, _breathingIntensity, (_elapsedTime / _timeToHitIntensity));
                WwiseEventHelper.SetRTPCValue(WwiseEventIDs.PLAYER_BREATHING_SPEED, _currentBreathingIntensity);
                _elapsedTime += Time.deltaTime;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            // Force update RTPC value
            WwiseEventHelper.SetRTPCValue(WwiseEventIDs.PLAYER_BREATHING_SPEED, _breathingIntensity);
        }
    }
}