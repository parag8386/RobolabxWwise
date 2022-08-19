namespace Robolab
{
	using Wwise.Events;
	using UnityEngine;

	public class Punch : MonoBehaviour
	{
		private static int s_punchParameterHash = Animator.StringToHash("Punch");

		private const string PLAYER_TAG = "Player";

		[SerializeField] private Animator _animator;

		void OnTriggerEnter(Collider other)
		{
			if (_animator.speed <= 0f)
			{
				return;
			}

			if (other.gameObject.tag == PLAYER_TAG)
			{
				if (WwiseEventHelper.PostEventID(WwiseEventIDs.PLAY_ROBOT_BATTERING_RAM, gameObject))
				{
					_animator.SetTrigger(s_punchParameterHash);
				}
			}
		}
	}
}