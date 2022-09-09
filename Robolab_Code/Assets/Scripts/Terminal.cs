namespace Robolab
{
    using System;
    using System.Collections;
    using Robolab.Story;
    using Robolab.Wwise.Events;
    using UnityEngine;
    using UnityStandardAssets.Characters.FirstPerson;

    [RequireComponent(typeof(Collider))]
    public class Terminal : MonoBehaviour
    {
        [SerializeField] private RigidbodyFirstPersonController _rigidbodyFirstPersonController = default;

        [SerializeField] private float _terminalActivationTime = 1.5f;

        [SerializeField] private int _terminalIndex = 0;

        private Collider _terminalTrigger;

        private bool _isTerminalActive = false;

        public event Action<int> OnTerminalActivationComplete;

        /// <summary>
        /// Fired when the player enteres the interaction range of a terminal
        /// </summary>
        /// <param name="bool"></param>Bool indicating whether the terminal can be interacted with
        /// <param name="int"></param>Int indicating the terminal number
        public event Action<bool, int> OnPlayerEnteredTerminalRange;

        private void Start()
        {
            _terminalTrigger = gameObject.GetComponent<Collider>();
            _terminalTrigger.isTrigger = true;
        }

        /// <summary>
        /// Allows the terminal to be interacted with
        /// </summary>
        /// <param name="interactable">True to use, else false</param>
        public void SetTerminalInteractable(bool interactable)
        {
            _isTerminalActive = interactable;
            _terminalTrigger.enabled = interactable;
        }

        /// <summary>
        /// Activates the terminal
        /// </summary>
        public void ActivateTerminal()
        {
            SetTerminalInteractable(false);
            _rigidbodyFirstPersonController.LockMovement = true;
            _rigidbodyFirstPersonController.LockLooking = true;

            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_TERMINALS, gameObject);
            WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_TYPING, gameObject);

            StartCoroutine(TerminalActivationCoroutine());
        }

        private IEnumerator TerminalActivationCoroutine()
        {
            yield return new WaitForSeconds(_terminalActivationTime);

            _rigidbodyFirstPersonController.LockMovement = false;
            _rigidbodyFirstPersonController.LockLooking = false;

            OnTerminalActivationComplete?.Invoke(_terminalIndex);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(StoryGameObjectReferences.PLAYER_TAG))
            {
                OnPlayerEnteredTerminalRange?.Invoke(_isTerminalActive, _terminalIndex);
            }
        }
    }
}