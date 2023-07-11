using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public struct PlayerEquipmentData : INetworkSerializable
{
    public PlayerItemData[] playerItems;
    public PlayerRuneData[] playerRunes;

    public PlayerEquipmentData(bool isSet)
    {
        playerItems = new PlayerItemData[5];
        playerRunes = new PlayerRuneData[5];
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        playerItems = new PlayerItemData[5];
        playerRunes = new PlayerRuneData[5];
        for (int i = 0; i < 5; i++)
        {
            playerItems[i] = new PlayerItemData();
            playerItems[i].NetworkSerialize(serializer);
        }

        for (int i = 0; i < 5; i++)
        {
            playerRunes[i] = new PlayerRuneData();
            playerRunes[i].NetworkSerialize(serializer);
        }
    }
}
