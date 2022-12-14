namespace Robolab.Story.Behaviour
{
    using UnityEngine;

    public class Story_Win : StoryBehaviourBase
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            _UIReferences.GameWinScreen.gameObject.SetActive(true);
            _UIReferences.HUDBase.SetActive(false);

            // Play win animation
            _storyGameObjectReferences.PostAnimation.Play("PostAdd");
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
