using UnityEngine;
using System.Collections;

public class AttackPlayer : MonoBehaviour
{
    private Animator animator;
    private ZombieHand hand;
    private int attackHash = Animator.StringToHash("Base Layer.atack01");

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        hand = this.GetComponentInChildren<ZombieHand>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == attackHash)
        {
            //hand.enabled = true;
            hand.attacking = true;
        }
        else
        {
            hand.attacking = false;
            //hand.enabled = false;
        }
    }
}
