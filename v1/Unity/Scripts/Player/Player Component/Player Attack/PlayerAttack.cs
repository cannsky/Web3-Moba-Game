using UnityEngine;

[System.Serializable]
public class PlayerAttack
{
    private Player player;
    
    public bool continuouslyCheckRange;

    private Player localTargetPlayer;

    private RaycastHit hit;
    private float targetDistance;

    public PlayerAttack(Player player) => this.player = player;

    public void OnUpdate()
    {
        if (player.IsClient) ClientOnUpdate();
        if (player.IsServer) ServerOnUpdate();
    }

    private void ClientOnUpdate()
    {
        if (player.playerInput.RightClick)
        {
            continuouslyCheckRange = false;
            localTargetPlayer = null;
            if (ClientCheckIsPlayerTargeted()) ClientCheckTargetPlayerRequirements();
        }

        if (localTargetPlayer != null && continuouslyCheckRange)
        {
            targetDistance = Vector3.Distance(player.transform.position, localTargetPlayer.transform.position);
            if (ClientCheckPlayerRange() && ClientCheckPlayerAttackCooldown()) ClientTryAttackRequest();
            else player.playerMovement.ClientTryMovementRequest(localTargetPlayer.transform.position);
        }
    }

    private void ServerOnUpdate() => ServerTryAttack();

    public void ClientCheckTargetPlayerRequirements()
    {
        localTargetPlayer = hit.collider.gameObject.GetComponent<Player>();

        if (localTargetPlayer == null) return;

        if (localTargetPlayer.playerData.Value.playerTeam == Player.Owner.playerData.Value.playerTeam)
        {
            player.playerMovement.ClientTryMovementRequest(localTargetPlayer.transform.position);
            localTargetPlayer = null;
        }
        else continuouslyCheckRange = true;
    }

    public void ClientTryAttackRequest()
    {
        player.PlayerAttackRequestServerRpc(localTargetPlayer.playerData.Value.playerID);
    }
    
    public void ServerTryAttack()
    {
        if (!ServerCheckIsPlayerAttacking()) return;
        if (!ServerCheckPlayerAttackCooldown()) return;
        if (!ServerCheckPlayerRange()) return;

        player.playerAttackData.Value = player.playerAttackData.Value.GeneratePlayerAttackData(playerLastAttackTime: Time.time, isPlayerAttacking: true);

        Player targetPlayer = ServerManager.Instance.players[player.playerAttackData.Value.playerTargetID];
        targetPlayer.playerData.Value = targetPlayer.playerData.Value.GeneratePlayerData("applyDamage", player.playerData.Value.playerADAttackDamage);

        player.PlayerAttackAnimationOrderClientRpc();
    }

    private bool ClientCheckIsPlayerTargeted() => Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Player"));
    private bool ClientCheckPlayerAttackCooldown() => Time.time - player.playerAttackData.Value.playerLastAttackTime >= player.playerData.Value.playerAttackCooldownTime;
    private bool ClientCheckPlayerRange() => targetDistance <= player.playerData.Value.playerChampionData.range;
    private bool ServerCheckPlayerAttackCooldown() => Time.time - player.playerAttackData.Value.playerLastAttackTime >= player.playerData.Value.playerAttackCooldownTime;
    private bool ServerCheckPlayerRange() => Vector3.Distance(player.transform.position, ServerManager.Instance.players[player.playerAttackData.Value.playerTargetID].transform.position) <= player.playerData.Value.playerChampionData.range;
    private bool ServerCheckIsPlayerAttacking() => player.playerAttackData.Value.isPlayerAttacking;
}