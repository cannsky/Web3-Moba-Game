using System.Linq;
using Unity.Netcode;

public class PlayerHealthData : INetworkSerializable
{
    public enum SumType { TotalHealth, HealthRegenerationSpeed, HealthStoleMultiplier }

    public float playerHealth;
    public float playerTotalHealth;
    public float playerHealthRegenerationSpeed;
    public float playerHealthStoleMultiplier;

    public PlayerHealthData()
    {
        playerHealth = 0;
        playerTotalHealth = 0;
        playerHealthRegenerationSpeed = 0;
        playerHealthStoleMultiplier = 0;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerHealth);
        serializer.SerializeValue(ref playerTotalHealth);
        serializer.SerializeValue(ref playerHealthRegenerationSpeed);
        serializer.SerializeValue(ref playerHealthStoleMultiplier);
    }

    public void GeneratePlayerHealthData(float playerHealth = -1, float playerTotalHealth = -1, float playerHealthRegenerationSpeed = -1, float playerHealthStoleMultiplier = -1)
    {
        this.playerHealth = playerHealth < 0 ? this.playerHealth : playerHealth;
        this.playerTotalHealth = playerTotalHealth < 0 ? this.playerTotalHealth : playerTotalHealth;
        this.playerHealthRegenerationSpeed = playerHealthRegenerationSpeed < 0 ? this.playerHealthRegenerationSpeed : playerHealthRegenerationSpeed;
        this.playerHealthStoleMultiplier = playerHealthStoleMultiplier < 0 ? this.playerHealthStoleMultiplier : playerHealthStoleMultiplier;
    }

    /*
    private float UpdateTotalHealth(Player player) => playerTotalHealth = player.playerChampionData.Value.totalHealth + SumExtraTotalHealth(player, SumType.TotalHealth);
    private float UpdateHealthRegenerationSpeed(Player player) => playerHealthRegenerationSpeed = player.playerChampionData.Value.healthRegenerationSpeed + (player.playerChampionData.Value.healthRegenerationSpeed * SumExtraTotalHealth(player, SumType.HealthRegenerationSpeed) / 100);
    private float UpdateHealthStoleMultiplier(Player player) => playerHealthStoleMultiplier = player.playerChampionData.Value.healthStoleMultiplier + SumExtraTotalHealth(player, SumType.HealthStoleMultiplier);
    */
    /*
    private float SumExtraTotalHealth(Player player, SumType sumType) => sumType switch
    {
        SumType.TotalHealth => player.playerEquipmentData.Value.playerItems.Sum(item => item.extraTotalHealth) + player.playerEquipmentData.Value.playerRunes.Sum(rune => rune.extraTotalHealth),
        SumType.HealthRegenerationSpeed => player.playerEquipmentData.Value.playerItems.Sum(item => item.extraHealthRegenerationSpeed) + player.playerEquipmentData.Value.playerRunes.Sum(rune => rune.extraHealthRegenerationSpeed),
        SumType.HealthStoleMultiplier => player.playerEquipmentData.Value.playerItems.Sum(item => item.extraHealthStoleMultiplier) + player.playerEquipmentData.Value.playerRunes.Sum(rune => rune.extraHealthStoleMultiplier),
        _ => 0
    };
    */
}