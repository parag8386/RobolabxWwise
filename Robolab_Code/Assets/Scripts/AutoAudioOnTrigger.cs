using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class AutoAudioOnTrigger : MonoBehaviour
{
    public AudioClip[] audioClips;

    public float mDelay = 0.0f;
    public float mDelayBetweenClips = 0.25f;

    private Rigidbody mRigidBody;

    private BoxCollider mBoxCollider;

    private AudioSource mAudioSource;

    private int mAudioIndex;

    private bool mPlayed = false;
    private bool mEntered = false;


	void Start ()
    {
        // Rigid body setup
        mRigidBody = gameObject.GetComponent<Rigidbody>();
        mRigidBody.isKinematic = true;
        mRigidBody.useGravity = false;

        // Collider setup
        mBoxCollider = gameObject.GetComponent<BoxCollider>();
        mBoxCollider.isTrigger = true;

        // Audio source setup
        mAudioSource = gameObject.GetComponent<AudioSource>();
        mAudioSource.playOnAwake = false;

        // May need this for potentially using multiple clips
        mAudioIndex = 0;

        mPlayed = false;
        mEntered = false;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            //Debug.LogError("In Trigger: " + gameObject.name + " and " + other.transform.gameObject.name);
            StartCoroutine(PlayVO());
            mEntered = true;
        }
    }

    private IEnumerator PlayVO()
    {
        // Audio Source
        if (mPlayed || mEntered)
            yield return null;
        else
        {
            yield return new WaitForSeconds(mDelay);

            while (mAudioIndex < audioClips.Length)
            {
                if (mAudioSource != null)
                {
                    if (!mAudioSource.isPlaying)
                    {
                        mAudioSource.PlayOneShot(audioClips[mAudioIndex]);

                        // Loop around on finish
                        if (mAudioIndex >= audioClips.Length)
                        {
                            mPlayed = true;
                            //mAudioIndex = 0;
                        }

                        yield return new WaitForSeconds(audioClips[mAudioIndex].length + mDelayBetweenClips);

                        mAudioIndex++;
                    }
                }
            }

            yield return null;
        }
    }
}
