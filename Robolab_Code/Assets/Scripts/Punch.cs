using UnityEngine;
using System.Collections;

public class Punch : MonoBehaviour
{
	private Animator anim;
	private int hitState;
	private int doPunchBool;

    private bool shutDown = false;
	
	
	void Awake ()
	{
		anim = GetComponent<Animator>();
		hitState = Animator.StringToHash("Base Layer.Hit");
		doPunchBool = Animator.StringToHash("DoPunch");
	}
	
	
	void OnTriggerEnter (Collider other)
	{
        if (!shutDown)
        {
            if (other.gameObject.tag == "Player")
                anim.SetBool(doPunchBool, true);
        }
	}
	
	void Update ()
	{
        //if (!shutDown)
        {
            //if (anim.GetCurrentAnimatorStateInfo(0).shortNameHash == hitState)
            //    anim.SetBool(doPunchBool, false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            shutDown = true;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            shutDown = false;
        }
	}
}
