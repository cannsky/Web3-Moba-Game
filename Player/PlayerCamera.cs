using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Vector3 offset;

    private void LateUpdate()
    {
        if (Player.ClientOwner != null) transform.position = Player.ClientOwner.transform.position + offset;
    }
}
