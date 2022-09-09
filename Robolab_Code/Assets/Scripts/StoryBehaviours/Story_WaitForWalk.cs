namespace Robolab.Story.Behaviour
{
    using Wwise.Events;
    using UnityEngine;
    using UnityStandardAssets.Characters.FirstPerson;

    public class Story_WaitForWalk : StoryBehaviourBase
    {
        [SerializeField] private float _minimumMoveTime = 1f;

        private float _timeMoving = 0f;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            _storyGameObjectReferences.RigidbodyFirstPersonController.LockMovement = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            // Check to see if the player has moved
            if (_storyGameObjectReferences.RigidbodyFirstPersonController.Velocity.sqrMagnitude > 0f)
            {
                _timeMoving += Time.deltaTime;
                if (_timeMoving >= _minimumMoveTime)
                {
                    TransitionToNextState(animator);
                }
            }
        }
    }
}