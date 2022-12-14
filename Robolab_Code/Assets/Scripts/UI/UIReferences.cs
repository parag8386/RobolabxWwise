namespace Robolab.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class UIReferences : MonoBehaviour
    {
        public GameObject HUDBase = default;

        public TextMeshProUGUI CountdownTimeDisplay = default;

        public UI_ObjectMarker ObjectMarker = default;
        public UI_Information InformationDisplay = default;
        public UI_SurroundMarker SurroundMarker = default;

        public Image GameLossScreen = default;
        public Image GameWinScreen = default;
    }
}