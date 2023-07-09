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

    public void OnStart()
    {
        if(player.IsServer) player.transform.position = new Vector3(10, 8, 8);
        agent = player.GetComponent<NavMeshAgent>();
    }

    public void OnUpdate()
    {
        if (player.IsOwner && player.playerInput.RightClick)
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                out hit, Mathf.Infinity, LayerMask.GetMask(terrainLayerName))) ClientTryMovementRequest(hit.point);
        ServerTryMove();
        ClientVisuals();
    }

    public void ClientTryMovementRequest(Vector3 movePosition)
    {
        if (!player.IsOwner) return;
        player.PlayerMovementRequestServerRpc(new Vector2(movePosition.x, movePosition.z), Time.time);
    }

    public void ServerTryMove()
    {
        if (!player.IsServer) return;
        SmoothRotate();
        if (!player.playerData.Value.isMoveRequested && player.playerData.Value.isMoving && agent.remainingDistance < 0.1f) StopMovement();
        if (!player.playerData.Value.isMoveRequested) return;
        agent.SetDestination(new Vector3(player.playerData.Value.playerMovementDestination.x, 0, player.playerData.Value.playerMovementDestination.y));
        player.playerData.Value = player.playerData.Value.GeneratePlayerData("isMoveRequested", false);
        player.playerData.Value = player.playerData.Value.GeneratePlayerData(PlayerData.PlayerAnimationState.Run);
    }

    private void SmoothRotate()
    {
        Vector3 direction = agent.velocity.normalized;
        direction.y = 0f;
        if(direction != Vector3.zero) player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(direction), playerRotationSpeed * Time.deltaTime);
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
