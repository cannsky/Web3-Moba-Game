using Unity.Netcode;
using UnityEngine;

public struct PlayerMovementData : INetworkSerializable
{
    public Vector2 playerMovementDestination;
    public float playerMovementTime;
    public bool isMoveRequested, isMoving;

    public PlayerMovementData(bool isSet)
    {
        playerMovementDestination = Vector2.zero;
        playerMovementTime = 0;
        isMoveRequested = false;
        isMoving = false;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerMovementDestination);
        serializer.SerializeValue(ref playerMovementTime);
        serializer.SerializeValue(ref isMoveRequested);
        serializer.SerializeValue(ref isMoving);
    }

    public PlayerMovementData GeneratePlayerMovementData(bool isMoveRequested = false, bool isMoving = false) => new PlayerMovementData()
    {
        playerMovementDestination = playerMovementDestination,
        playerMovementTime = playerMovementTime,
        isMoveRequested = isMoveRequested,
        isMoving = isMoving
    };

    public PlayerMovementData GeneratePlayerMovementData(Vector2 playerMovementDestination, float playerMovementTime = 0, bool isMoveRequested = false, bool isMoving = false) => new PlayerMovementData()
    {
        playerMovementDestination = playerMovementDestination,
        playerMovementTime = playerMovementTime,
        isMoveRequested = isMoveRequested,
        isMoving = isMoving
    };
}
