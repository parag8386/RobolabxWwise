namespace Robolab.Story.Behaviour
{
    using UnityEngine;

    public class Story_Alarm : Story_PlaySound
    {
        [SerializeField] private Color _alarmColorForLights = Color.red;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            for (int i = 0; i < _storyGameObjectReferences.LightObjects.Length; i++)
            {
                _storyGameObjectReferences.LightObjects[i].color = _alarmColorForLights;
            }

            // Turn on all lights
            for (int i = 0; i < _storyGameObjectReferences.LightObjects.Length; i++)
            {
                _storyGameObjectReferences.LightObjects[i].enabled = true;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
        }
    }
}
