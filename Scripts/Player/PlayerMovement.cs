using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public LayerMask terrainMask;

    private PlayerInput playerInput;
    private Vector2 playerMovementDestination;
    private float playerMovementTime;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (playerInput.RightClick)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, terrainMask))
            {
                agent.SetDestination(hit.point);
                playerMovementDestination = new Vector2(hit.point.x, hit.point.z);
                playerMovementTime = Time.time;
            }
        }
    }
}
