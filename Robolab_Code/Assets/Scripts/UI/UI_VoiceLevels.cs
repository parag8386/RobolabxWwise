namespace Robolab.UI
{
    using Robolab.Wwise.Events;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class UI_VoiceLevels : MonoBehaviour
    {
        private const float VOICE_LEVEL_MAX_VALUE_FROM_WWISE = 48f;

        [SerializeField]
        private RectTransform _volumeBarTransform = default;

        [SerializeField]
        private RectTransform[] _volumeSubBars = default;

        private float _rawVolume = 0f;
        private float _volume = 0f;

        private void LateUpdate()
        {
            // Get RTPC value
            WwiseEventHelper.GetRTPCValue(WwiseEventIDs.LEVEL_METER, out _rawVolume);

            if (_rawVolume >= VOICE_LEVEL_MAX_VALUE_FROM_WWISE)
            {
                _rawVolume = -VOICE_LEVEL_MAX_VALUE_FROM_WWISE;
            }

            _volume = (_rawVolume + VOICE_LEVEL_MAX_VALUE_FROM_WWISE);
            _volume = Mathf.Clamp(_volume, 1f, VOICE_LEVEL_MAX_VALUE_FROM_WWISE);

            Vector2 sizeDelta = new Vector2(_volumeBarTransform.sizeDelta.x, _volume);
            _volumeBarTransform.sizeDelta = sizeDelta;

            float middle = (_volumeSubBars.Length - 1) / 2f;
            for (int i = 0; i < _volumeSubBars.Length; i++)
            {
                float divider = Mathf.Abs(middle - i);
                divider = Mathf.Clamp(divider, 1, middle);

                Vector2 subBarSizeDelta = new Vector2(_volumeBarTransform.sizeDelta.x, _volume / divider);
                _volumeSubBars[i].sizeDelta = subBarSizeDelta;
            }
        }
    }
}