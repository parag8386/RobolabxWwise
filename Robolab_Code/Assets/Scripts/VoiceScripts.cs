using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceScripts : MonoBehaviour
{
    public AudioClip[] audioClips;

    public AudioSource audioSource;

    private int mAudioFileIndex = 0;


    private void Update()
    {
        if (audioClips.Length > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (audioClips[mAudioFileIndex] != null)
                {
                    if (!audioSource.isPlaying)
                    {
                        audioSource.PlayOneShot(audioClips[mAudioFileIndex]);
                        mAudioFileIndex++;
                        if (mAudioFileIndex >= audioClips.Length)
                        {
                            mAudioFileIndex = 0;
                        }
                    }
                }
            }
        }
    }
}
