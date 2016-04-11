using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    public int health;
    [SerializeField]
    private GameObject effect;

    private bool invinsible;
    private Color color;
    private int remainingInvinsibilityFrames;

    // Use this for initialization
    void Start ()
    {
        invinsible = false;
        color = GetComponent<Renderer>().material.color;
        remainingInvinsibilityFrames = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(health <= 0)
        {
            Instantiate(effect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
        if(invinsible)
        {
            if(remainingInvinsibilityFrames <= 0)
            {
                ToggleInvinsible();
            }
            else
            {
                remainingInvinsibilityFrames -= 1;
            }
        }
        
	}

    void ToggleInvinsible()
    {
        invinsible = !invinsible;
        if (invinsible)
        {
            remainingInvinsibilityFrames = 60;
            color.a = 0.5f;
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        else
        {
            remainingInvinsibilityFrames = 0;
            color.a = 1f;
            GetComponent<Renderer>().material.SetColor("_Color", color);
        }
        //GetComponent<Renderer>().material.SetColor("_Color", color);
    }

    public bool TakeDamage(int damage)
    {
        if(invinsible)
        {
            return false;
        }
        else
        {
            health -= damage;
            ToggleInvinsible();
            return true;
        }
    }
}
