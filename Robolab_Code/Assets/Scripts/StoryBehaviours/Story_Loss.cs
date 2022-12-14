namespace Robolab.Story.Behaviour
{
    using UnityEngine;

    public class Story_Loss : StoryBehaviourBase
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            _UIReferences.GameLossScreen.gameObject.SetActive(true);
            _UIReferences.HUDBase.SetActive(false);

            // Game over condition
            _storyGameObjectReferences.RigidbodyFirstPersonController.LockLooking = true;
            _storyGameObjectReferences.RigidbodyFirstPersonController.LockMovement = true;

            // Play loss aniamtion
            _storyGameObjectReferences.CameraAnimation.Play("Camera_Loss");
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
