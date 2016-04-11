using UnityEngine;
using System.Collections;

public class AttackEnemy : MonoBehaviour
{
    private Animator animator;
    private int attackHash = Animator.StringToHash("attacking");
    private int swingStateHash = Animator.StringToHash("Base Layer.swordSwing2");
    private bool attacking = false;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger(attackHash);
        }
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        attacking = stateInfo.fullPathHash == swingStateHash;
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
