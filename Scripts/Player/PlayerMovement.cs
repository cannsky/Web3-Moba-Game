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
    private Vector2 playerMovementDestination;
    private float playerMovementTime, playerRotationSpeed;
    private bool isMoving;
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
        if (player.playerInput.RightClick)
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                out hit, Mathf.Infinity, LayerMask.GetMask(terrainLayerName))) TryMove(hit.point);
        if (isMoving && agent.remainingDistance < 0.1f) StopMovement();
        if (isMoving) SmoothRotate();
    }

    public void TryMove(Vector3 movePosition)
    {
        isMoving = true;
        agent.SetDestination(movePosition);

        var tempStruct = player.playerData.Value;
        tempStruct.playerMovementDestination = new Vector2(hit.point.x, hit.point.z);
        tempStruct.playerMovementTime = Time.time;
        player.playerData.Value = tempStruct;
        Debug.Log(player.playerData.Value.playerMovementDestination);
        player.playerInput.RightClick = false;
        player.playerAnimator.PlayRunAnimation(true);
        Vector3 direction = movePosition - player.transform.position;
        direction.y = 0f;
        targetRotation = Quaternion.LookRotation(direction);
    }

    private void SmoothRotate()
    {
        if (Quaternion.Angle(player.transform.rotation, targetRotation) > 0.1f)
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, playerRotationSpeed * Time.deltaTime);
    }


    public void StopMovement()
    {
        isMoving = false;
        player.playerAnimator.PlayRunAnimation(false);
    }
}
