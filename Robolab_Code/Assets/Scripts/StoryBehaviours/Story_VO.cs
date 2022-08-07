namespace Robolab.Story.Behaviour
{
    using Robolab.Wwise.Events;
    using UnityEngine;

    public class Story_VO : StoryBehaviourBase
    {
        [SerializeField] private string _VOEventID = default;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            // Post VO event
            WwiseEventHelper.PostEventID(_VOEventID, _storyGameObjectReferences.NPC);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            // Stop VO
            WwiseEventHelper.StopEventID(_VOEventID);
        }
    }
}
