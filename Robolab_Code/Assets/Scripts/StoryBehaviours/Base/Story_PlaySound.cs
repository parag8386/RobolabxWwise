namespace Robolab.Story.Behaviour
{
    using Robolab.Wwise.Events;
    using UnityEngine;

    public class Story_PlaySound : StoryBehaviourBase
    {
        [SerializeField] private string _SoundEventID = default;

        [SerializeField] private bool _StopOnExit = true;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            // Post event
            WwiseEventHelper.PostEventID(_SoundEventID, _storyGameObjectReferences.GenericAmbience);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            // Force stop event
            if (_StopOnExit)
            {
                WwiseEventHelper.StopEventID(_SoundEventID);
            }
        }
    }
}
