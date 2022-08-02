using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class AudioOnTrigger : MonoBehaviour
{
    public AudioClip[] audioClips;

    private Rigidbody mRigidBody;

    private BoxCollider mBoxCollider;

    private AudioSource mAudioSource;

    public AudioSource pAudioSource { get { return mAudioSource; } }

    private bool mInTrigger;

    private int mAudioIndex;

    public int pAudioIndex { get { return mAudioIndex; } set { mAudioIndex = value; } }


	void Start ()
    {
        mRigidBody = gameObject.GetComponent<Rigidbody>();
        mRigidBody.isKinematic = true;
        mRigidBody.useGravity = false;

        mBoxCollider = gameObject.GetComponent<BoxCollider>();
        mBoxCollider.isTrigger = true;

        mAudioSource = gameObject.GetComponent<AudioSource>();
        mAudioSource.playOnAwake = false;

        mInTrigger = false;

        mAudioIndex = 0;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            Debug.LogError("In Trigger: " + gameObject.name + " and " + other.transform.gameObject.name);
            mInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            Debug.LogError("Out Trigger: " + gameObject.name + " and " + other.transform.gameObject.name);
            mInTrigger = false;
        }
    }

    private void Update()
    {
        if (mInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                if (!mAudioSource.isPlaying)
                {
                    mAudioSource.PlayOneShot(audioClips[mAudioIndex]);
                    mAudioIndex++;

                    if (mAudioIndex >= audioClips.Length)
                        mAudioIndex = 0;
                }
            }
        }
    }
}
