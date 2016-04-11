using UnityEngine;
using System.Collections;

public class SwingSword : MonoBehaviour {

    public GameObject sword;
    //private Transform trans;
    
    // Use this for initialization
	void Start ()
    {
        //trans.localEulerAngles = new Vector3(90, 0, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetButton("Fire1"))
        {
            GameObject go = (GameObject)Instantiate(sword, this.transform.position, this.transform.rotation);
            go.transform.parent = transform;
        }
	}
}
