using UnityEngine;

[System.Serializable]
public class PlayerAnimator
{
    private Player player;
    private Animator animator;

    public PlayerAnimator(Player player) => this.player = player;

    public void OnStart() => animator = player.transform.GetChild(0).GetComponent<Animator>();

    public void PlayRunAnimation(bool isRunning) => animator.SetBool("Run", isRunning);

    public void PlayAttackAnimation(string animationName) => animator.Play(animationName);
}