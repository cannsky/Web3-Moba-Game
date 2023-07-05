using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public NavMeshAgent agent;

    private PlayerInput playerInput;
    private RaycastHit hit;
    private Vector2 playerMovementDestination;
    private float playerMovementTime;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (playerInput.RightClick) TryMove();
    }

    private void TryMove()
    {
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Terrain"))) return;
        agent.SetDestination(hit.point);
        playerMovementDestination = new Vector2(hit.point.x, hit.point.z);
        playerMovementTime = Time.time;
        playerInput.RightClick = false;
    }
}
