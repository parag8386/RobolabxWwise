namespace Robolab.Story.Behaviour
{
    using Robolab.UI;
    using UnityEngine;

    public class StoryBehaviourBase : StateMachineBehaviour
    {
        private const string STORY_STATE_INDEX_PARAMETER = "Story_State_Index";

        [SerializeField, Tooltip("Set to -1 to disable the timed exit")] protected float _stayInStateForSeconds = 2f;
        [SerializeField] protected int _storyStateIndexToTransitionTo = -1;

        protected StoryGameObjectReferences _storyGameObjectReferences = null;
        protected UIReferences _UIReferences = null;

        protected float _timeInState = 0f;
        protected int _storyStateIndexParameterHash = Animator.StringToHash(STORY_STATE_INDEX_PARAMETER);

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            // Init
            Init(animator);
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // Init
            Init(animator);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_stayInStateForSeconds < 0)
            {
                // If the _stayInStateForSeconds variable is less than 0, disable the time in 
                // state check
                return;
            }

            // Switch states once the time in state has expired
            if (_timeInState <= 0f)
            {
                TransitionToNextState(animator);
            }
            else
            {
                _timeInState -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                _timeInState = 0.1f;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash) { }

        private void Init(Animator animator)
        {
            // Init references to story objects
            if (_storyGameObjectReferences == null)
            {
                _storyGameObjectReferences = animator.gameObject.GetComponent<StoryGameObjectReferences>();
            }

            // Init references to story objects
            if (_UIReferences == null)
            {
                _UIReferences = animator.gameObject.GetComponent<UIReferences>();
            }

            // Init time in state variable
            _timeInState = _stayInStateForSeconds;
        }

        protected void TransitionToNextState(Animator animator)
        {
            animator.SetInteger(_storyStateIndexParameterHash, _storyStateIndexToTransitionTo);
        }
    }
}