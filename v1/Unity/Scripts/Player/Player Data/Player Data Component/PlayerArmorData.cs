using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerArmorData : INetworkSerializable
{
    public float playerADArmor;
    public float playerAPArmor;

    public PlayerArmorData()
    {
        playerADArmor = 0;
        playerAPArmor = 0;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerADArmor);
        serializer.SerializeValue(ref playerAPArmor);
    }

    public void GeneratePlayerAttackData(float playerADArmor = -1, float playerAPArmor = -1)
    {
        this.playerADArmor = playerADArmor < 0 ? this.playerADArmor : playerADArmor;
        this.playerAPArmor = playerAPArmor < 0 ? this.playerAPArmor : playerAPArmor;
    }
}
