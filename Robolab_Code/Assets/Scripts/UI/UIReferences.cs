namespace Robolab.UI
{
    using Robolab.Utils;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class UIReferences : MonoBehaviour
    {
        public TextMeshProUGUI CountdownTimeDisplay = default;
        public FollowGameObjectInScreenSpace TerminalMarker = default;

        public Image GameLossScreen = default;
        public Image GameWinScreen = default;
    }
}