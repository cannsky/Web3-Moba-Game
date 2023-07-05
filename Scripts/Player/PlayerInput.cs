using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool LeftClick { get; private set; }
    public bool RightClick { get; private set; }
    public bool Q { get; private set; }
    public bool W { get; private set; }
    public bool E { get; private set; }
    public bool R { get; private set; }

    void Update()
    {
        LeftClick = Input.GetMouseButtonDown(0);
        RightClick = Input.GetMouseButtonDown(1);
        Q = Input.GetKeyDown(KeyCode.Q);
        W = Input.GetKeyDown(KeyCode.W);
        E = Input.GetKeyDown(KeyCode.E);
        R = Input.GetKeyDown(KeyCode.R);
    }

    void LateUpdate()
    {
        LeftClick = false;
        RightClick = false;
        Q = false;
        W = false;
        E = false;
        R = false;
    }
}
