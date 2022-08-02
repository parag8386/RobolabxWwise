using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	
	public float rotateSpeed = 5f;

    private bool rotate = true;

    public AudioSource audioSource;

	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            rotate = false;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            rotate = true;
        }

        if (rotate)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();

            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
        }
        else
        {
            audioSource.Stop();
        }
	}
}
