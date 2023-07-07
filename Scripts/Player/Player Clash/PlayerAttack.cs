using UnityEngine;

[System.Serializable]
public class PlayerAttack
{
    private Player player;
    
    public bool continuouslyCheckRange;

    private Player targetPlayer;

    //private PlayerVFX playerVFX;
    //private PlayerAudio playerAudio;

    private RaycastHit hit;
    private float attackCooldownTime = 0f, targetDistance;
    private bool isAttacking = false;

    public PlayerAttack(Player player) => this.player = player;

    public void OnUpdate()
    {
        if (player.playerInput.RightClick)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Player")))
            {
                targetPlayer = hit.collider.gameObject.GetComponent<Player>();

                if (targetPlayer == null) return;

                if (targetPlayer.playerData.Value.playerTeam == Player.Owner.playerData.Value.playerTeam)
                {
                    player.playerMovement.TryMove(targetPlayer.transform.position);
                    targetPlayer = null;
                }
                else continuouslyCheckRange = true;
            }
        }

        if (targetPlayer != null && continuouslyCheckRange)
        {
            targetDistance = Vector3.Distance(player.transform.position, targetPlayer.transform.position);
            if (targetDistance <= Player.Owner.playerData.Value.playerChampion.range)
            {
                continuouslyCheckRange = false;
                player.playerMovement.StopMovement();
                AttackTarget();
            }
            else player.playerMovement.TryMove(targetPlayer.transform.position);
        }
    }

    private void AttackTarget()
    {
        if (Time.time - Player.Owner.playerData.Value.playerLastAttackTime >= Player.Owner.playerData.Value.playerAttackCooldownTime)
        {
            if (!isAttacking) // WILL BE IMPLEMENTED LATER: CANNOT DO ATTACK & SKILL AT THE SAME TIME.
            {
                player.playerAnimator.PlayAttackAnimation("Normal Attack");
                //playerVFX.InstantiateAttackVFX();
                //playerAudio.PlayAttackSound();

                // Apply attack logic here

                // Set attack cooldown time
                var tempStruct = Player.Owner.playerData.Value;
                tempStruct.playerLastAttackTime = Time.time;
                Player.Owner.playerData.Value = tempStruct;
            }
        }
    }
}