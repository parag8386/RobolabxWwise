using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmAudioSequence : MonoBehaviour
{
    public AudioClip AudioRumble;
    public AudioClip AudioRedLight;
    public AudioClip AudioRedLight2;
    public AudioClip AudioRedLight3;

    public AudioSource sourceRumble;

    public float alarmGapTime = 6.0f;
    public float gapBetweenAlarms = 0.25f;
    
    private Coroutine redCoroutine_;

    private bool poweredDown = false;
    private bool maxAlarms = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Turn on alarm state
            sourceRumble.Stop();
            redCoroutine_ = StartCoroutine(DelayedLightRed());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // Alarm stage 2
            sourceRumble.Stop();
            StopCoroutine(redCoroutine_);
            Invoke("PlayAlarmStage2", gapBetweenAlarms);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            // Alarm stage 3 
            sourceRumble.Stop();
            StopCoroutine(redCoroutine_);
            Invoke("PlayAlarmStage3", gapBetweenAlarms);
            maxAlarms = true;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            // Reset 
            StopAllCoroutines();
            sourceRumble.Stop();
        }
    }

    private IEnumerator DelayedLightRed()
    {
        if (!poweredDown)
        {
            // Play Sound for when all lights go off
            sourceRumble.clip = AudioRumble;
            sourceRumble.loop = false;
            sourceRumble.Play();

            yield return new WaitForSeconds(alarmGapTime);
            poweredDown = true;
        }

        if (maxAlarms)
        {
            yield return new WaitForSeconds(gapBetweenAlarms);
            maxAlarms = false;
        }

        sourceRumble.clip = AudioRedLight;
        sourceRumble.loop = true;
        sourceRumble.Play();
        //sourceMusic.PlayOneShot(AudioRedLight);
    }

    private void PlayAlarmStage2()
    {
        sourceRumble.clip = AudioRedLight2;
        sourceRumble.loop = true;
        sourceRumble.Play();
    }

    private void PlayAlarmStage3()
    {
        sourceRumble.clip = AudioRedLight3;
        sourceRumble.loop = true;
        sourceRumble.Play();
    }
}
