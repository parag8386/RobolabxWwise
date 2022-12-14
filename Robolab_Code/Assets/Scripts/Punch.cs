namespace Robolab
{
	using Wwise.Events;
	using UnityEngine;
	using Robolab.Story;
	using Robolab.UI;

	public class Punch : MonoBehaviour
	{
		private static int s_punchParameterHash = Animator.StringToHash("Punch");

		private const string PLAYER_TAG = "Player";

		[SerializeField] private UIReferences _UIReferences;

		[SerializeField] private Animator _animator;

		private bool canPlay = false;

		private void Update()
		{
            if (_animator.speed <= 0f)
            {
                return;
            }

			if (canPlay && Input.GetKeyDown(KeyCode.F))
			{
				if (WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_ROBOT_BATTERING_RAM, gameObject))
				{
					_animator.SetTrigger(s_punchParameterHash);
				}
			}
        }

		private void OnTriggerEnter(Collider other)
		{
			if (_animator.speed <= 0f)
			{
				return;
			}

			if (other.gameObject.tag == PLAYER_TAG)
			{
				canPlay = true;
				_UIReferences.InformationDisplay.SetInformation("Press [F] to interact", 0f);
			}
		}

		private void OnTriggerExit(Collider other)
		{
            if (other.gameObject.tag == PLAYER_TAG)
            {
				canPlay = false;
                _UIReferences.InformationDisplay.SetInformation(null, -1f);
            }
        }
	}
}