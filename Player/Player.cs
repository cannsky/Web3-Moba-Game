using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player ClientOwner;

    private void Awake()
    {
        ClientOwner = this;
    }
}
