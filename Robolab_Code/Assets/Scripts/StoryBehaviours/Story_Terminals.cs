namespace Robolab.Story.Behaviour
{
    using Wwise.Events;
    using UnityEngine;

    public class Story_Terminals : Story_LightFlicker
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            _timeInState = float.MaxValue;

            WwiseEventHelper.PostEventID(WwiseEventIDs.STOP_ALARM_01_LOOP, _storyGameObjectReferences.GenericAmbience);
            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_ALARM_02_LOOP, _storyGameObjectReferences.GenericAmbience);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);


        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
        }
    }
}
