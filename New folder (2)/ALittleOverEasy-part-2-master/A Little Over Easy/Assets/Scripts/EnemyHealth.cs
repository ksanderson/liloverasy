using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    public int health;
    [SerializeField]
    private GameObject effect;

    private bool invinsible;
    private int remainingInvinsibilityFrames;
    [SerializeField]
    private int invinsibilityFrames;
    [SerializeField]
    private float timeFromDeathToDespawn;

    private Rigidbody rigidBody;
    private NavMeshAgent pathAgent;
    private FollowScript followScript;
    private CapsuleCollider capsuleCollider;
    private Animator animator;
    private int dieHash = Animator.StringToHash("Die");
    private int damageHash = Animator.StringToHash("Damage");
    private int walkingHash = Animator.StringToHash("Walking");
    private int creepUpAnimationHash = Animator.StringToHash("Base Layer.creep up");
    //private int walk3AnimationHash = Animator.StringToHash("Base Layer.walk03");
    private int damageAnimationHash = Animator.StringToHash("Base Layer.damege");
    private bool alive;

    private AnimatorStateInfo previousStateInfo;
    private AnimatorStateInfo currentStateInfo;

    // Use this for initialization
    void Start ()
    {
        invinsible = false;
        alive = true;
        //color = GetComponent<Renderer>().material.color;
        remainingInvinsibilityFrames = 0;

        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        pathAgent = GetComponent<NavMeshAgent>();
        followScript = GetComponent<FollowScript>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        followScript.enabled = false;
        pathAgent.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        followScript.enabled = true;
        pathAgent.enabled = true;

        if (health <= 0 && alive)
        {
            //Instantiate(effect, this.transform.position, Quaternion.identity);

            pathAgent.enabled = false;
            followScript.enabled = false;
            alive = false;
            capsuleCollider.enabled = false;
            animator.SetTrigger(dieHash);
            StartCoroutine(Die());
        }
        else if (!alive || currentStateInfo.fullPathHash == creepUpAnimationHash || currentStateInfo.fullPathHash == damageAnimationHash)
        {
            pathAgent.enabled = false;
            followScript.enabled = false;
        }

        if (pathAgent.velocity.magnitude > 0.49)
        {
            animator.SetBool(walkingHash, true);
        }
        else
        {
            animator.SetBool(walkingHash, false);
        }
        
        if(invinsible)
        {
            if(remainingInvinsibilityFrames <= 0)
            {
                ToggleInvinsible();
                Color transparent = Color.white;
                transparent.a = 0;
                setColor(transparent);
            }
            else
            {
                remainingInvinsibilityFrames -= 1;
            }
        }
    }

    void LateUpdate()
    {
        currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (previousStateInfo.fullPathHash == creepUpAnimationHash && currentStateInfo.fullPathHash != creepUpAnimationHash)
        {
            capsuleCollider.enabled = true;
            this.transform.Translate(1.25f * Vector3.down);
            //rigidBody.useGravity = true;
        }
        previousStateInfo = animator.GetCurrentAnimatorStateInfo(0);
    }

    void ToggleInvinsible()
    {
        invinsible = !invinsible;
        if (invinsible)
        {
            remainingInvinsibilityFrames = invinsibilityFrames;
            //color.a = 0.5f;
            //GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        else
        {
            remainingInvinsibilityFrames = 0;
            //color.a = 1f;
            //GetComponent<Renderer>().material.SetColor("_Color", color);
        }
        //GetComponent<Renderer>().material.SetColor("_Color", color);
    }

    public bool TakeDamage(int damage)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (invinsible || stateInfo.fullPathHash == creepUpAnimationHash)
        {
            return false;
        }
        else
        {
            setColor(Color.red);

            health -= damage;
            if(health > 0)
            {
                pathAgent.enabled = false;
                followScript.enabled = false;
                animator.SetTrigger(damageHash);
            }
            ToggleInvinsible();
            return true;
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(timeFromDeathToDespawn);
        Destroy(gameObject);
    }

    private void setColor(Color color)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = color;
        }
    }
}
