using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ServerOnClientDisconnectCallback : ServerCallback
{
    public override void InitializeCallback() => NetworkManager.Singleton.OnClientDisconnectCallback += (id) => Callback();

    public override void Callback()
    {
        if (!ServerManager.Instance.IsServer) return;
        ServerManager.Instance.DecreasePlayerCount();
    }
}
