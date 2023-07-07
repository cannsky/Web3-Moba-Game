using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void Start()
    {
        NetworkManager.Singleton.StartHost();
    }
}
