using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudioOnTrigger : MonoBehaviour
{
    public AudioClip[] audioClips;

    public AudioOnTrigger otherNPCAudio;
    
    private AudioSource mAudioSource;
    
    private int mAudioIndex;


	void Start ()
    {
        mAudioSource = gameObject.GetComponent<AudioSource>();
        mAudioSource.playOnAwake = false;

        if (mAudioIndex != 0)
            mAudioIndex = 0;
	}

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    Debug.LogError(gameObject.name);
        //    if (!otherNPCAudio.pAudioSource.isPlaying)
        //    {
        //        otherNPCAudio.pAudioSource.PlayOneShot(otherNPCAudio.audioClips[otherNPCAudio.pAudioIndex]);
        //        mAudioSource.PlayOneShot(audioClips[otherNPCAudio.pAudioIndex]);
        //        otherNPCAudio.pAudioIndex++;

        //        if (otherNPCAudio.pAudioIndex >= otherNPCAudio.audioClips.Length)
        //            otherNPCAudio.pAudioIndex = 0;
        //    }

        //    //if (!mAudioSource.isPlaying)
        //    //{
        //    //    mAudioIndex = otherNPCAudio.pAudioIndex;
        //    //    mAudioSource.PlayOneShot(audioClips[mAudioIndex]);

        //    //    if (mAudioIndex >= audioClips.Length)
        //    //        mAudioIndex = 0;
        //    //}
        //}
    }
}
