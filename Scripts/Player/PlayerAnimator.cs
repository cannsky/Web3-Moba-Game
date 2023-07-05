using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    public Champion champion;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = champion.animatorController;
    }

    public void Run()
    {
        animator.SetTrigger("Run");
    }

    public void Idle()
    {
        animator.ResetTrigger("Run");
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
}