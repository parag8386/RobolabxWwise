namespace Robolab.Story.Behaviour
{
    using Robolab.Wwise.Events;
    using UnityEngine;

    public class Story_LightRecover : StoryBehaviourBase
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_POWERUP, _storyGameObjectReferences.GenericAmbience);

            // Turn on all lights
            _storyGameObjectReferences.RestoreLightColorsToDefault();
            for (int i = 0; i < _storyGameObjectReferences.LightObjects.Length; i++)
            {
                _storyGameObjectReferences.LightObjects[i].enabled = true;
            }

            // Start aniamtors
            _storyGameObjectReferences.RobotArmAnimator.Update(0);
            _storyGameObjectReferences.RobotArmAnimator.speed = 1f;

            _storyGameObjectReferences.BatteringRamAnimator.Update(0);
            _storyGameObjectReferences.BatteringRamAnimator.speed = 1f;

            _storyGameObjectReferences.LegacyFanAnimation.Play();

            // Start particles
            for (int i = 0; i < _storyGameObjectReferences.FanParticles.Length; i++)
            {
                _storyGameObjectReferences.FanParticles[i].Play(withChildren: true);
                _storyGameObjectReferences.FanParticles[i].gameObject.SetActive(true);
            }

            // Start ambience
            // Post music event
            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_MUSIC_LOOP, _storyGameObjectReferences.RetroTelevision);

            // Post ambience events
            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_FAN_LOOP, _storyGameObjectReferences.Fan);
            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_GLASSPAD_LOOP, _storyGameObjectReferences.HoverPad);
            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_AMBIENCE_LOOP, _storyGameObjectReferences.GenericAmbience);
            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_INDOOR_LAB_LOOP, _storyGameObjectReferences.StaticRobotLab);
            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_FAN_BLADE_LOOP, _storyGameObjectReferences.Fan);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
        }
    }
}