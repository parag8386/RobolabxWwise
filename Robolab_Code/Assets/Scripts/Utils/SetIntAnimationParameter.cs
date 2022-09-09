namespace Robolab.Utils
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SetIntAnimationParameter : StateMachineBehaviour
    {
        [SerializeField] private string _parameterName = default;

        [SerializeField] private int _valueToSet = default;

        [SerializeField] private float _delay = -1f;

        private float _timeInState = 0f;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            _timeInState = _delay;
            if (_delay <= 0f)
            {
                animator.SetInteger(_parameterName, _valueToSet);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            if (_timeInState > 0f)
            {
                _timeInState -= Time.deltaTime;
                if (_timeInState <= 0f)
                {
                    animator.SetInteger(_parameterName, _valueToSet);
                }
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
        }
    }
}