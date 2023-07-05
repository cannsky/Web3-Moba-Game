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

    public void Run() => animator.SetBool("Run", true);

    public void Idle() => animator.SetBool("Run", false);

    public void Attack() => animator.SetTrigger("Attack");
}