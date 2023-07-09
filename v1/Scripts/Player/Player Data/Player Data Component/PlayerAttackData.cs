using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public struct PlayerAttackData : INetworkSerializable
{
    public int playerTargetID;
    public float playerLastAttackTime;
    public bool isPlayerAttacking;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerTargetID);
        serializer.SerializeValue(ref playerLastAttackTime);
        serializer.SerializeValue(ref isPlayerAttacking);
    }
}
