namespace Robolab.Story.Behaviour
{
    using Robolab.UI;
    using Robolab.Wwise.Events;
    using UnityEngine;

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
            _UIReferences.ObjectMarker.SetMarkerTarget(_storyGameObjectReferences.Terminals[_terminalToActivate].transform);
            _UIReferences.SurroundMarker.SetTarget(_storyGameObjectReferences.Terminals[_terminalToActivate].transform);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            
            _UIReferences.CountdownTimeDisplay.text = _timeInState.ToString("00.00");

            if  (_isAtCorrectTerminal)
            {
                _UIReferences.InformationDisplay.SetInformation("Press [F] to interact", 1f);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    _isAtCorrectTerminal = false;

                    // Activate terminal
                    _storyGameObjectReferences.Terminals[_terminalToActivate].ActivateTerminal();
                    _storyGameObjectReferences.Terminals[_terminalToActivate].OnTerminalActivationComplete += TerminalActivated;
                }
            }
            else
            {
                _UIReferences.InformationDisplay.SetInformation(null, -1f);
            }

            if (_terminalToActivate >= _storyGameObjectReferences.Terminals.Length)
            {
                // Continue game
                _storyStateIndexToTransitionTo = SUCCESS_STATE_INDEX;
                _timeInState = 0f;
                _UIReferences.ObjectMarker.SetMarkerTarget(null);
                _UIReferences.SurroundMarker.SetTarget(null);
                _UIReferences.CountdownTimeDisplay.gameObject.SetActive(false);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            _UIReferences.ObjectMarker.SetMarkerTarget(null);
            _UIReferences.SurroundMarker.SetTarget(null);
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
                _UIReferences.ObjectMarker.SetMarkerTarget(_storyGameObjectReferences.Terminals[_terminalToActivate].transform);
                _UIReferences.SurroundMarker.SetTarget(_storyGameObjectReferences.Terminals[_terminalToActivate].transform);
            }
        }

        private void PlayerEnteredTerminalRange(bool isTerminalActive, int terminalIndex)
        {
            _isAtCorrectTerminal = (isTerminalActive && (terminalIndex == _terminalToActivate));
        }
    }
}