using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class PlayerMovement
{
    private Player player;

    public NavMeshAgent agent;
    private RaycastHit hit;
    private Quaternion targetRotation;
    private float playerRotationSpeed;
    private string terrainLayerName;

    public PlayerMovement(Player player)
    {
        this.player = player;
        playerRotationSpeed = player.playerSettings.playerRotationSpeed;
        terrainLayerName = player.playerSettings.terrainLayerName;
    }

    public void OnStart() => agent = player.GetComponent<NavMeshAgent>();

    public void OnUpdate()
    {
        if (player.IsOwner && player.playerInput.RightClick)
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                out hit, Mathf.Infinity, LayerMask.GetMask(terrainLayerName))) ClientTryMoveInput(hit.point);
        ServerTryMove();
        ClientVisuals();
    }

    public void ClientTryMoveInput(Vector3 movePosition)
    {
        if (!player.IsOwner) return;
        player.UpdatePlayerMovementServerRpc(new Vector2(movePosition.x, movePosition.z), Time.time);
        player.playerInput.RightClick = false;
    }

    public void ServerTryMove()
    {
        if (!player.IsServer) return;
        SmoothRotate();
        if (!player.playerData.Value.isMoveRequested && player.playerData.Value.isMoving && agent.remainingDistance < 0.1f) StopMovement();
        if (!player.playerData.Value.isMoveRequested) return;
        agent.SetDestination(new Vector3(player.playerData.Value.playerMovementDestination.x, 0, player.playerData.Value.playerMovementDestination.y));
        Vector3 direction = new Vector3(player.playerData.Value.playerMovementDestination.x, 0, player.playerData.Value.playerMovementDestination.y) - player.transform.position;
        direction.y = 0f;
        targetRotation = Quaternion.LookRotation(direction);
        Debug.Log(player.playerData.Value.playerMovementDestination);
        player.playerData.Value = player.playerData.Value.GeneratePlayerData("isMoveRequested", false);
        player.playerData.Value = player.playerData.Value.GeneratePlayerData(PlayerData.PlayerAnimationState.Run);
    }

    private void SmoothRotate()
    {
        if (Quaternion.Angle(player.transform.rotation, targetRotation) > 0.1f)
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, playerRotationSpeed * Time.deltaTime);
    }

    private void ClientVisuals()
    {
        if (player.playerData.Value.playerAnimationState == PlayerData.PlayerAnimationState.Idle) player.playerAnimator.PlayRunAnimation(false);
        else if (player.playerData.Value.playerAnimationState == PlayerData.PlayerAnimationState.Run) player.playerAnimator.PlayRunAnimation(true);
        else if (player.playerData.Value.playerAnimationState == PlayerData.PlayerAnimationState.Attack) player.playerAnimator.PlayAttackAnimation("Normal Attack");
    }

    public void StopMovement()
    {
        player.playerData.Value = player.playerData.Value.GeneratePlayerData("isMoving", false);
        player.playerData.Value = player.playerData.Value.GeneratePlayerData(PlayerData.PlayerAnimationState.Idle);
    }
}
