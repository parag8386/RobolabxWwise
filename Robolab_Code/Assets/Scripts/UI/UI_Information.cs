namespace Robolab.UI
{
    using UnityEngine;
    using TMPro;

    public class UI_Information : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _informationField = default;

        private float _hash = -float.MaxValue;

        public void SetInformation(string information, float infoHash)
        {
            if (_hash == infoHash)
            {
                return;
            }

            _hash = infoHash;
            _informationField.gameObject.SetActive(!string.IsNullOrEmpty(information));
            _informationField.text = information;
        }
    }
}