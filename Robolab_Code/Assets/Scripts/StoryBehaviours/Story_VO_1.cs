namespace Robolab.Story.Behaviour
{
    using Robolab.Wwise.Events;
    using UnityStandardAssets.Characters.FirstPerson;
    using UnityEngine;

    public class Story_VO_1 : StoryBehaviourBase
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            // Post VO_1 event
            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_VO_F_LINE01, _storyGameObjectReferences.NPC);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            // Stop VO_1 and unlock movement
            WwiseEventHelper.StopEventID(WwiseEventIDs.PLAY_VO_F_LINE01);
            RigidbodyFirstPersonController.s_LockMovement = false;
        }
    }
}
