using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool LeftClick { get; set; }
    public bool RightClick { get; set; }
    public bool Q { get; set; }
    public bool W { get; set; }
    public bool E { get; set; }
    public bool R { get; set; }

    public PlayerInputActions playerInputActions;

    private void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Movement.LeftClick.performed += i => LeftClick = true;
        playerInputActions.Movement.RightClick.performed += i => RightClick = true;
        playerInputActions.Enable();
    }

    private void LateUpdate()
    {
        LeftClick = false;
        RightClick = false;
        Q = false;
        W = false;
        E = false;
        R = false;
    }
}
