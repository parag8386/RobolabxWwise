using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudioSequence : MonoBehaviour
{
    public UnityEngine.Audio.AudioMixer ambientMixer;

    public AudioSource powerUpSource;

    public AudioClip powerUpClip;

    public float gapBeforeFirstPowerUp = 2.0f;
    public float gapBeforeFullPowerUp = 0.25f;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ambientMixer.FindSnapshot("Powerdown").TransitionTo(0.0f);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Invoke("InitialPowerup", gapBeforeFirstPowerUp);
        }
    }

    private void InitialPowerup()
    {
        powerUpSource.PlayOneShot(powerUpClip);
        Invoke("Powerup", gapBeforeFullPowerUp);
    }

    private void Powerup()
    {
        ambientMixer.FindSnapshot("Snapshot").TransitionTo(0.0f);
    }
}
