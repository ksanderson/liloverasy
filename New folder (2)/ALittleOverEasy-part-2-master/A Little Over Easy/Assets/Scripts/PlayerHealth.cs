using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    public int health;
    [SerializeField]
    private GameObject effect;

    private bool invinsible;
    private int remainingInvinsibilityFrames;
    [SerializeField]
    private int invinsibilityFrames;

    private Rigidbody rigidBody;
    private bool alive;

    // Use this for initialization
    void Start()
    {
        invinsible = false;
        alive = true;
        //color = GetComponent<Renderer>().material.color;
        remainingInvinsibilityFrames = 0;

        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invinsible)
        {
            if (remainingInvinsibilityFrames <= 0)
            {
                ToggleInvinsible();
                setColor(Color.gray);
            }
            else
            {
                remainingInvinsibilityFrames -= 1;
            }
        }
        if (health <= 0)
        {
            health = 0;
        }
    }

    void ToggleInvinsible()
    {
        invinsible = !invinsible;
        if (invinsible)
        {
            remainingInvinsibilityFrames = invinsibilityFrames;
        }
        else
        {
            remainingInvinsibilityFrames = 0;
        }
    }

    public bool TakeDamage(int damage)
    {
        if (invinsible)
        {
            return false;
        }
        else
        {
            setColor(Color.red);

            health -= damage;
            ToggleInvinsible();
            return true;
        }
    }

    private void setColor(Color color)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            if (!(renderer is SpriteRenderer))
            {
                renderer.material.color = color;
            }
        }
    }
}
