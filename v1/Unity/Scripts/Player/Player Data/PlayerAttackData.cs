using Unity.Netcode;

public struct PlayerAttackData : INetworkSerializable
{
    public int playerTargetID;
    public float playerLastAttackTime;
    public bool isPlayerAttacking;

    public PlayerAttackData(bool isSet)
    {
        playerTargetID = 0;
        playerLastAttackTime = 0;
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

    public PlayerAttackData GeneratePlayerAttackData(float playerLastAttackTime = 0, bool isPlayerAttacking = false) => new PlayerAttackData()
    {
        playerTargetID = playerTargetID,
        playerLastAttackTime = playerLastAttackTime,
        isPlayerAttacking = isPlayerAttacking
    };

    public PlayerAttackData GeneratePlayerAttackData(int playerTargetID = 0, float playerLastAttackTime = 0, bool isPlayerAttacking = false) => new PlayerAttackData()
    {
        playerTargetID = playerTargetID,
        playerLastAttackTime = playerLastAttackTime,
        isPlayerAttacking = isPlayerAttacking
    };
}
