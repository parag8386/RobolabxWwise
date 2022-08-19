namespace Robolab.Story.Behaviour
{
    using Robolab.Wwise.Events;
    using UnityEngine;
    using UnityEngine.Animations;

    public class Story_LightShutdown : StoryBehaviourBase
    {

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_POWERDOWN_01, _storyGameObjectReferences.GenericAmbience);

            // Turn off all lights
            for (int i = 0; i < _storyGameObjectReferences.LightObjects.Length; i++)
            {
                _storyGameObjectReferences.LightObjects[i].enabled = false;
            }

            // Stop aniamtors
            _storyGameObjectReferences.RobotArmAnimator.Update(0);
            _storyGameObjectReferences.RobotArmAnimator.speed = 0f;

            _storyGameObjectReferences.BatteringRamAnimator.Update(0);
            _storyGameObjectReferences.BatteringRamAnimator.speed = 0f;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
        }
    }
}