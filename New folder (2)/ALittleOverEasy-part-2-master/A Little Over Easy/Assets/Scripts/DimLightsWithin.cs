using UnityEngine;
using System.Collections;

public class DimLightsWithin : MonoBehaviour
{
    [SerializeField]
    private GameObject sun;
    private Light sunlight;
    private bool playerWithin;
    
    // Use this for initialization
	void Start ()
    {
        playerWithin = false;
        sunlight = sun.GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (playerWithin && sunlight.intensity > 0.0f)
        {
            sunlight.intensity -= 0.01f;
        }
        else if (!playerWithin && sunlight.intensity < 1.0f)
        {
            sunlight.intensity += 0.01f;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            playerWithin = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            playerWithin = false;
        }
    }
}
