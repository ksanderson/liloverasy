using UnityEngine;
using System.Collections;

public class ZombieHand : MonoBehaviour {

    //private BoxCollider handHitBox;
    public bool attacking;

    // Use this for initialization
    void Start () {
        //handHitBox = this.GetComponent<BoxCollider>();
        attacking = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (attacking && other.tag.Equals("Player"))
        {
            if (other.GetComponent<PlayerHealth>().TakeDamage(1))
            {
                //HIT!
                Debug.Log("Hit the Player!");
            }
            else
            {
                //ting!
            }
            attacking = false;
            //enabled = false;
        }
    }
}
