using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ServerManager : NetworkBehaviour
{
    public static ServerManager Instance;

    private NetworkVariable<int> playerCount = new NetworkVariable<int>();
    private Player[] players = new Player[10];

    private List<ServerCallback> callbacks = new List<ServerCallback>() { new ServerOnClientConnectedCallback(), new ServerOnClientDisconnectCallback() };

    private void Awake() => Instance = this;

    private void Start() { foreach (ServerCallback callback in callbacks) callback.InitializeCallback(); }

    public void IncreasePlayerCount() => playerCount.Value++;

    public void DecreasePlayerCount() => playerCount.Value--;
}
