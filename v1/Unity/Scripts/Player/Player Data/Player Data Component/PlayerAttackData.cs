using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static PlayerData;
using static UnityEditor.Progress;

public struct PlayerAttackData : INetworkSerializable
{
    public bool isSet;
    public int playerTargetID;
    public float playerLastAttackTime;
    public bool isPlayerAttacking;

    public PlayerAttackData(bool isSet)
    {
        this.isSet = isSet;
        playerTargetID = 0;
        playerLastAttackTime = 0f;
        isPlayerAttacking = false;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerTargetID);
        serializer.SerializeValue(ref playerLastAttackTime);
        serializer.SerializeValue(ref isPlayerAttacking);
    }

    public PlayerAttackData GeneratePlayerAttackData(bool isPlayerAttacking = false) => new PlayerAttackData()
    {
        playerTargetID = playerTargetID,
        playerLastAttackTime = playerLastAttackTime,
        isPlayerAttacking = isPlayerAttacking
    };

    public PlayerAttackData GeneratePlayerAttackData(int playerTargetID = 0, bool isPlayerAttacking = false) => new PlayerAttackData()
    {
        playerTargetID = playerTargetID,
        playerLastAttackTime = playerLastAttackTime,
        isPlayerAttacking = isPlayerAttacking
    };

    public PlayerAttackData GeneratePlayerAttackData(float playerLastAttackTime = 0f, bool isPlayerAttacking = false) => new PlayerAttackData()
    {
        playerTargetID = playerTargetID,
        playerLastAttackTime = playerLastAttackTime,
        isPlayerAttacking = isPlayerAttacking
    };

    public PlayerAttackData GeneratePlayerAttackData(int playerTargetID = 0, float playerLastAttackTime = 0f, bool isPlayerAttacking = false) => new PlayerAttackData()
    {
        playerTargetID = playerTargetID,
        playerLastAttackTime = playerLastAttackTime,
        isPlayerAttacking = isPlayerAttacking
    };
}
