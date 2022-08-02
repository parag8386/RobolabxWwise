using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSequence : MonoBehaviour
{
    public float alarmGapTime = 6.0f;
    public float lightRestoreInitialDelay = 0.25f;
    public float lightRstoreRecurringDelay = 0.025f;

    private List<GameObject> lights_ = new List<GameObject>();

    private Coroutine flickerCoroutine_;
    private Coroutine redCoroutine_;

    private Vector2 lightRange;

    private Color lightColor;
    private float lightColorTemp;
    

	void Start ()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name.Contains("realtime") || transform.GetChild(i).gameObject.name.Contains("baked"))
            {
                lights_.Add(transform.GetChild(i).gameObject);
            }
        }

        lightColor = lights_[0].GetComponent<Light>().color;
        lightColorTemp = lights_[0].GetComponent<Light>().colorTemperature;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //if (redCoroutine_ == null)
            {
                // Toggle flicker
                if (flickerCoroutine_ != null)
                {
                    for (int i = 0; i < lights_.Count; i++)
                    {
                        lights_[i].SetActive(true);
                    }
                    StopCoroutine(flickerCoroutine_);
                    flickerCoroutine_ = null;
                }
                else
                {
                    lightRange = GetLightChunk();
                    flickerCoroutine_ = StartCoroutine(DelayedLightFlicker());
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Turn on alarm state
            if (flickerCoroutine_ != null)
            {
                for (int i = 0; i < lights_.Count; i++)
                {
                    lights_[i].SetActive(false);
                }
                StopCoroutine(flickerCoroutine_);
                flickerCoroutine_ = null;
                redCoroutine_ = StartCoroutine(DelayedLightRed());
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            for (int i = 0; i < lights_.Count; i++)
            {
                lights_[i].SetActive(false);
            }
            StopAllCoroutines();
            flickerCoroutine_ = null;
            redCoroutine_ = null;
            StartCoroutine(DelayedLightRestore());
        }
    }

    private IEnumerator DelayedLightFlicker()
    {
        // Flicker Routine
        while (true)
        {
            for (int i = Mathf.FloorToInt(lightRange.x); i <= Mathf.FloorToInt(lightRange.y); i++)
            {
                lights_[i].SetActive(!lights_[i].activeSelf);
            }

            yield return new WaitForSeconds(Random.Range(0.05f, 0.3f));

            for (int i = Mathf.FloorToInt(lightRange.x); i <= Mathf.FloorToInt(lightRange.y); i++)
            {
                lights_[i].SetActive(!lights_[i].activeSelf);
            }

            lightRange = GetLightChunk();
        }
        //
    }

    private IEnumerator DelayedLightRestore()
    {
        yield return new WaitForSeconds(lightRestoreInitialDelay);

        // Switch all lights to red
        for (int i = 0; i < lights_.Count; i++)
        {
            lights_[i].GetComponent<Light>().color = lightColor;
            lights_[i].GetComponent<Light>().colorTemperature = lightColorTemp;
            lights_[i].SetActive(true);

            yield return new WaitForSeconds(lightRstoreRecurringDelay);
        }

        yield return null;
    }

    private IEnumerator DelayedLightRed()
    {
        yield return new WaitForSeconds(alarmGapTime);

        // Switch all lights to red
        for (int i = 0; i < lights_.Count; i++)
        {
            lights_[i].GetComponent<Light>().color = Color.red;
            lights_[i].GetComponent<Light>().colorTemperature = 0;
            lights_[i].SetActive(true);
        }
    }

    private Vector2 GetLightChunk()
    {
        int min, max = -1;
        
        min = Random.Range(0, lights_.Count - 2);

        do
        {
            max = Random.Range(1, lights_.Count);
        }
        while (max <= min);

        return new Vector2(min, max);
    }
}
