using UnityEngine;
using System.Collections;

public class AttackEnemy : MonoBehaviour
{
    private Animator animator;
    private int attackHash = Animator.StringToHash("attacking");
    private int swingStateHash = Animator.StringToHash("Base Layer.swordSwing2");
    private bool attacking = false;
    [SerializeField]
    private int attackFrames;
    private int remainingAttackFrames = 0;
    private int coolDown = 0;
    [SerializeField]
    private int maxCoolDown;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDown <= 0 && Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger(attackHash);
            remainingAttackFrames = attackFrames;
            attacking = true;
            coolDown = maxCoolDown;
        }
        else if (remainingAttackFrames > 0)
        {
            remainingAttackFrames--;
        }
        else
        {
            attacking = false;
        }
        if (coolDown > 0)
        {
            coolDown--;
        }
        //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //attacking = stateInfo.fullPathHash == swingStateHash;
    }

    void OnTriggerStay(Collider other)
    {
        if (attacking && other.tag.Equals("Enemy"))
        {
            if(other.GetComponent<EnemyHealth>().TakeDamage(1))
            {
                //slash!
            }
            else
            {
                //ting!
            }
        }
    }
}
