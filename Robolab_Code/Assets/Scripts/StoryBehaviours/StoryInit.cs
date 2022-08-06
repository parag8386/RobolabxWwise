namespace Robolab.Story.Behaviour
{
    using Robolab.Wwise.Events;
    using UnityEngine;

    public class StoryInit : StateMachineBehaviour
    {
        StoryGameObjectReferences _storyGameObjectReferences = null;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            InitReferences(animator);

            // Post music event
            AkSoundEngine.PostEvent(WwiseEventIDs.PLAY_MUSIC_LOOP, _storyGameObjectReferences.RetroTelevision);

            // Post ambience events
            AkSoundEngine.PostEvent(WwiseEventIDs.PLAY_FAN_LOOP, _storyGameObjectReferences.Fan);
            AkSoundEngine.PostEvent(WwiseEventIDs.PLAY_GLASSPAD_LOOP, _storyGameObjectReferences.HoverPad);
            AkSoundEngine.PostEvent(WwiseEventIDs.PLAY_AMBIENCE_LOOP, _storyGameObjectReferences.GenericAmbience);
            AkSoundEngine.PostEvent(WwiseEventIDs.PLAY_INDOOR_LAB_LOOP, _storyGameObjectReferences.StaticRobotLab);
            AkSoundEngine.PostEvent(WwiseEventIDs.PLAY_FAN_BLADE_LOOP, _storyGameObjectReferences.Fan);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        private void InitReferences(Animator animator)
        {
            if (_storyGameObjectReferences == null)
            {
                _storyGameObjectReferences = animator.gameObject.GetComponent<StoryGameObjectReferences>();
            }
        }
    }
}
