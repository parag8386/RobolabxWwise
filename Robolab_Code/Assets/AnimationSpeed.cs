using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeed : MonoBehaviour {

    public float speed = 2.0f;

    public AnimationClip clip;
    public Animation legacyAnimation;

	// Use this for initialization
	void Start () {
        legacyAnimation[clip.name].speed = speed;
	}
	
	// Update is called once per frame
	void Update () {
        if (legacyAnimation[clip.name].speed != speed)
            legacyAnimation[clip.name].speed = speed;
    }
}
