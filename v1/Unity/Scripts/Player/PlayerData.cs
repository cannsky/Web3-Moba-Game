using Unity.Netcode;

public class PlayerData : INetworkSerializable
{
    public enum PlayerTeam { TeamBlue, TeamRed }

    public PlayerTeam playerTeam;

    public PlayerAnimationData playerAnimationData;
    public PlayerArmorData playerArmorData;
    public PlayerAttackData playerAttackData;
    public PlayerChampionData playerChampionData;
    public PlayerDamageData playerDamageData;
    public PlayerHealthData playerHealthData;
    public PlayerMovementData playerMovementData;

    public int playerID;
    public bool isSet;

    public PlayerData() 
    {
        playerChampionData = new PlayerChampionData();
        playerAnimationData = new PlayerAnimationData();
        playerArmorData = new PlayerArmorData();
        playerAttackData = new PlayerAttackData();
        playerDamageData = new PlayerDamageData();
        playerHealthData = new PlayerHealthData();
        playerMovementData = new PlayerMovementData();
    }

    public PlayerData(Champion networkChampion)
    {
        playerChampionData = new PlayerChampionData(networkChampion);
        playerAnimationData = new PlayerAnimationData();
        playerArmorData = new PlayerArmorData();
        playerAttackData = new PlayerAttackData();
        playerDamageData = new PlayerDamageData();
        playerHealthData = new PlayerHealthData();
        playerMovementData = new PlayerMovementData();
        isSet = true;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerID);
        serializer.SerializeValue(ref isSet);
        serializer.SerializeValue(ref playerTeam);
        playerAnimationData.NetworkSerialize(serializer);
        playerArmorData.NetworkSerialize(serializer);
        playerAttackData.NetworkSerialize(serializer);
        playerDamageData.NetworkSerialize(serializer);
        playerMovementData.NetworkSerialize(serializer);
    }
}
