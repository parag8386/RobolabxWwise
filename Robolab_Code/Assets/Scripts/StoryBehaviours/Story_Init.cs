namespace Robolab.Story.Behaviour
{
    using Robolab.Wwise.Events;
    using UnityEngine;

    public class Story_Init : StoryBehaviourBase
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            // Post music event
            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_MUSIC_LOOP, _storyGameObjectReferences.RetroTelevision);

            // Post ambience events
            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_FAN_LOOP, _storyGameObjectReferences.Fan);
            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_GLASSPAD_LOOP, _storyGameObjectReferences.HoverPad);
            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_AMBIENCE_LOOP, _storyGameObjectReferences.GenericAmbience);
            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_INDOOR_LAB_LOOP, _storyGameObjectReferences.StaticRobotLab);
            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_FAN_BLADE_LOOP, _storyGameObjectReferences.Fan);

            // Lock movement
            _storyGameObjectReferences.RigidbodyFirstPersonController.LockMovement = true;
            _storyGameObjectReferences.RigidbodyFirstPersonController.LockLooking = true;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            _storyGameObjectReferences.RigidbodyFirstPersonController.LockLooking = false;
        }
    }
}
