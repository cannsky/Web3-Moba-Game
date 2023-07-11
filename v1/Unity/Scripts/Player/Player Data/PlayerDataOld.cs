using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public struct PlayerDataOld : INetworkSerializable
{
    public enum PlayerDataType { Animation, Attack, Movement }
    public enum PlayerTeam { TeamBlue, TeamRed }
    
    /*
     * Please remember that the data being transferred to the player (or client) as well.
     * Since this is a web3 project, all data (maybe some will be hashed) will be visible to the other players.
     * If you are planning to make it web2 server, consider that some data shouldn't be known by the other player.
     */

    public int playerID;
    public PlayerTeam playerTeam;
    public PlayerChampionData playerChampionData;
    public PlayerItemData[] items;
    public PlayerRuneData[] runes;
    public float playerTotalMana;
    public float playerManaRegenerationSpeed;
    public float playerMana;
    public float playerAttackCooldownTime;
    public float playerADAttackDamage;
    public float playerAPAttackDamage;
    public float playerADArmor;
    public float playerAPArmor;
    public float playerADArmorPiercing;
    public float playerAPArmorPiercing;
    public float playerMovementSpeed;
    public bool isSet;

    public PlayerDataOld(int ID, PlayerChampionData networkChampion, PlayerTeam team)
    {
        playerID = ID;
        isSet = true;
        playerTeam = team;
        playerChampionData = networkChampion;
        items = new PlayerItemData[5]; for (int i = 0; i < items.Length; i++) items[i] = new PlayerItemData();
        runes = new PlayerRuneData[5]; for (int i = 0; i < runes.Length; i++) runes[i] = new PlayerRuneData();
        playerTotalMana = playerManaRegenerationSpeed = playerMana = 0f;
        playerAttackCooldownTime = playerADAttackDamage = playerAPAttackDamage = 0f;
        playerADArmor = playerAPArmor = playerADArmorPiercing = playerAPArmorPiercing = 0f;
        playerMovementSpeed = 0f;
        isSet = true;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerID);
        serializer.SerializeValue(ref isSet);
        serializer.SerializeValue(ref playerTeam);
        serializer.SerializeValue(ref playerTotalMana);
        serializer.SerializeValue(ref playerManaRegenerationSpeed);
        serializer.SerializeValue(ref playerMana);
        serializer.SerializeValue(ref playerAttackCooldownTime);
        serializer.SerializeValue(ref playerADAttackDamage);
        serializer.SerializeValue(ref playerAPAttackDamage);
        serializer.SerializeValue(ref playerADArmor);
        serializer.SerializeValue(ref playerAPArmor);
        serializer.SerializeValue(ref playerADArmorPiercing);
        serializer.SerializeValue(ref playerAPArmorPiercing);
        serializer.SerializeValue(ref playerMovementSpeed);
        playerChampionData.NetworkSerialize(serializer);

        /*
         * NetworkSerializer is not getting items or runes set? We set it again.
         */

        items = new PlayerItemData[5]; for (int i = 0; i < items.Length; i++) items[i] = new PlayerItemData();
        runes = new PlayerRuneData[5]; for (int i = 0; i < runes.Length; i++) runes[i] = new PlayerRuneData();
        foreach (PlayerItemData item in items) item.NetworkSerialize(serializer);
        foreach (PlayerRuneData rune in runes) rune.NetworkSerialize(serializer);
    }

    private float UpdateTotalMana() => playerTotalMana = playerChampionData.totalMana + SumExtraTotalMana();
    private float UpdateManaRegenerationSpeed() => playerManaRegenerationSpeed = playerChampionData.manaRegenerationSpeed + (playerChampionData.manaRegenerationSpeed * SumExtraManaRegenerationSpeed() / 100);
    private float UpdateAttackCooldownTime() => playerAttackCooldownTime = playerChampionData.attackCooldownTime - (playerChampionData.attackCooldownTime * SumExtraAttackSpeed() / 100);
    private float UpdateADAttackDamage() => playerADAttackDamage = playerChampionData.adAttackDamage + (playerChampionData.adAttackDamage * SumExtraADAttackDamage() / 100);
    private float UpdateAPAttackDamage() => playerAPAttackDamage = playerChampionData.apAttackDamage + (playerChampionData.apAttackDamage * SumExtraAPAttackDamage() / 100);
    private float UpdateADArmor() => playerADArmor = playerChampionData.adArmor + SumExtraADArmor();
    private float UpdateAPArmor() => playerAPArmor = playerChampionData.apArmor + SumExtraAPArmor();
    private float UpdateADArmorPiercing() => playerADArmorPiercing = playerChampionData.adArmorPiercing + (playerChampionData.adArmorPiercing * SumExtraADArmorPiercing() / 100);
    private float UpdateAPArmorPiercing() => playerAPArmorPiercing = playerChampionData.apArmorPiercing + (playerChampionData.apArmorPiercing * SumExtraAPArmorPiercing() / 100);
    private float UpdateMovementSpeed() => playerMovementSpeed = playerChampionData.movementSpeed + (playerChampionData.movementSpeed * SumExtraMovementSpeed() / 100);
    private float SumExtraTotalMana() => items.Sum(item => item.extraTotalMana) + runes.Sum(rune => rune.extraTotalMana);
    private float SumExtraManaRegenerationSpeed() => items.Sum(item => item.extraManaRegenerationSpeed) + runes.Sum(rune => rune.extraManaRegenerationSpeed);
    private float SumExtraAttackSpeed() => items.Sum(item => item.extraAttackSpeed) + runes.Sum(rune => rune.extraAttackSpeed);
    private float SumExtraADAttackDamage() => items.Sum(item => item.extraADAttackDamage) + runes.Sum(rune => rune.extraADAttackDamage);
    private float SumExtraAPAttackDamage() => items.Sum(item => item.extraAPAttackDamage) + runes.Sum(rune => rune.extraAPAttackDamage);
    private float SumExtraADArmor() => items.Sum(item => item.extraADArmor) + runes.Sum(rune => rune.extraADArmor);
    private float SumExtraAPArmor() => items.Sum(item => item.extraAPArmor) + runes.Sum(rune => rune.extraAPArmor);
    private float SumExtraADArmorPiercing() => items.Sum(item => item.extraADArmorPiercing) + runes.Sum(rune => rune.extraADArmorPiercing);
    private float SumExtraAPArmorPiercing() => items.Sum(item => item.extraAPArmorPiercing) + runes.Sum(rune => rune.extraAPArmorPiercing);
    private float SumExtraMovementSpeed() => items.Sum(item => item.extraMovementSpeed) + runes.Sum(rune => rune.extraMovementSpeed);

    public PlayerDataOld GeneratePlayerData() => new PlayerDataOld()
    {
        playerID = playerID,
        playerTeam = playerTeam,
        playerChampionData = playerChampionData,
        items = items,
        runes = runes,
        playerTotalMana = playerTotalMana,
        playerManaRegenerationSpeed = playerManaRegenerationSpeed,
        playerMana = playerMana,
        playerAttackCooldownTime = playerAttackCooldownTime,
        playerADAttackDamage = playerADAttackDamage,
        playerAPAttackDamage = playerAPAttackDamage,
        playerADArmor = playerADArmor,
        playerAPArmor = playerAPArmor,
        playerADArmorPiercing = playerADArmorPiercing,
        playerAPArmorPiercing = playerAPArmorPiercing,
        playerMovementSpeed = playerMovementSpeed,
    };

    public PlayerDataOld UpdatePlayerData() => new PlayerDataOld()
    {
        playerID = playerID,
        isSet = isSet,
        playerTeam = playerTeam,
        playerChampionData = playerChampionData,
        items = items,
        runes = runes,
        playerTotalMana = UpdateTotalMana(),
        playerManaRegenerationSpeed = UpdateManaRegenerationSpeed(),
        playerMana = playerMana,
        playerAttackCooldownTime = UpdateAttackCooldownTime(),
        playerADAttackDamage = UpdateADAttackDamage(),
        playerAPAttackDamage = UpdateAPAttackDamage(),
        playerADArmor = UpdateADArmor(),
        playerAPArmor = UpdateAPArmor(),
        playerADArmorPiercing = UpdateADArmorPiercing(),
        playerAPArmorPiercing = UpdateAPArmorPiercing(),
        playerMovementSpeed = UpdateMovementSpeed(),
    };
}