namespace Robolab.Story.Behaviour
{
    using System;
    using Robolab.UI;
    using Robolab.Wwise.Events;
    using UnityEngine;
    using UnityEngine.Animations;

    public class Story_Terminals : StoryBehaviourBase
    {
        private const int SUCCESS_STATE_INDEX = 10;

        private const string TERMINAL_2_VO_ID = "Play_VO_F_Line05";
        private const string TERMINAL_3_VO_ID = "Play_VO_F_Line06";

        private int _terminalToActivate = 0;

        private bool _isAtCorrectTerminal = false;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            for (int i = 0; i < _storyGameObjectReferences.Terminals.Length; i++)
            {
                _storyGameObjectReferences.Terminals[i].OnPlayerEnteredTerminalRange += PlayerEnteredTerminalRange;
            }

            // Activate first terminal
            _storyGameObjectReferences.Terminals[_terminalToActivate].SetTerminalInteractable(true);
            _UIReferences.CountdownTimeDisplay.gameObject.SetActive(true);
            _UIReferences.TerminalMarker.gameObject.SetActive(true);
            _UIReferences.TerminalMarker.TextMesh.text = $"{_terminalToActivate + 1}";
            _UIReferences.TerminalMarker.UpdateTarget(_storyGameObjectReferences.Terminals[_terminalToActivate].transform);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            
            _UIReferences.CountdownTimeDisplay.text = _timeInState.ToString("00.00");

            if  (_isAtCorrectTerminal)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    _isAtCorrectTerminal = false;

                    // Activate terminal
                    _storyGameObjectReferences.Terminals[_terminalToActivate].ActivateTerminal();
                    _storyGameObjectReferences.Terminals[_terminalToActivate].OnTerminalActivationComplete += TerminalActivated;
                }
            }

            if (_terminalToActivate >= _storyGameObjectReferences.Terminals.Length)
            {
                // Continue game
                _storyStateIndexToTransitionTo = SUCCESS_STATE_INDEX;
                _timeInState = 0f;
                _UIReferences.CountdownTimeDisplay.gameObject.SetActive(false);
                _UIReferences.TerminalMarker.gameObject.SetActive(false);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            for (int i = 0; i < _storyGameObjectReferences.Terminals.Length; i++)
            {
                _storyGameObjectReferences.Terminals[i].OnPlayerEnteredTerminalRange -= PlayerEnteredTerminalRange;
            }
        }

        private void TerminalActivated(int terminalIndex)
        {
            _storyGameObjectReferences.Terminals[terminalIndex].OnTerminalActivationComplete -= TerminalActivated;

            switch (terminalIndex)
            {
                case 0:
                    WwiseEventHelper.PlayVO(TERMINAL_2_VO_ID, _storyGameObjectReferences.NPC);
                    break;

                case 1:
                    WwiseEventHelper.PlayVO(TERMINAL_3_VO_ID, _storyGameObjectReferences.NPC);
                    break;
            }

            // Enable next terminal
            _terminalToActivate++;
            if (_terminalToActivate < _storyGameObjectReferences.Terminals.Length)
            {
                _storyGameObjectReferences.Terminals[_terminalToActivate].SetTerminalInteractable(true);
                _UIReferences.TerminalMarker.UpdateTarget(_storyGameObjectReferences.Terminals[_terminalToActivate].transform);
                _UIReferences.TerminalMarker.TextMesh.text = $"{_terminalToActivate + 1}";
            }
        }

        private void PlayerEnteredTerminalRange(bool isTerminalActive, int terminalIndex)
        {
            _isAtCorrectTerminal = (isTerminalActive && (terminalIndex == _terminalToActivate));
        }
    }
}