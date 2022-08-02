using AK.Wwise;
using System.Collections;
using UnityEngine;

public class LightAudioSequence : MonoBehaviour
{
    private const string Play_Lights_Flicker_Event = "Play_Lights_Flicker";
    private const string Play_Lights_On_Event = "Play_Lights_On";

    private uint flickerEventPlayingId_ = 0;
    private uint lightsOnEventPlayingId_ = 0;

    private Coroutine flickerCoroutine_;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Toggle flicker
            if (flickerCoroutine_ != null)
            {
                StopLightFlicker();
            }
            else
            {
                // Stop Alarm
                AkSoundEngine.StopPlayingID(lightsOnEventPlayingId_);
                lightsOnEventPlayingId_ = 0;

                flickerCoroutine_ = StartCoroutine(DelayedLightFlicker());
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Turn on alarm state
            if (lightsOnEventPlayingId_ == 0)
            {
                StopLightFlicker();
                lightsOnEventPlayingId_ = AkSoundEngine.PostEvent(Play_Lights_On_Event, gameObject);
            }
        }
    }

    private IEnumerator DelayedLightFlicker()
    {
        // Flicker Routine
        while (true)
        {
            // Flicker Audio
            if (flickerEventPlayingId_ == 0)
            {
                flickerEventPlayingId_ = AkSoundEngine.PostEvent(Play_Lights_Flicker_Event, gameObject);
            }
            yield return new WaitForSeconds(Random.Range(0.05f, 1f));
        }
    }

    private void StopLightFlicker()
    {
        AkSoundEngine.StopPlayingID(flickerEventPlayingId_);
        StopCoroutine(flickerCoroutine_);
        flickerCoroutine_ = null;
        flickerEventPlayingId_ = 0;
    }
}
