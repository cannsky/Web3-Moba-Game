using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct PlayerData : INetworkSerializable
{
    public enum PlayerTeam { TeamBlue, TeamRed }
    public enum PlayerAnimationState { Idle, Run, Attack, SkillQ, SkillW, SkillE, SkillR }
    /*
     * Please remember that the data being transferred to the player (or client) as well.
     * Since this is a web3 project, all data (maybe some will be hashed) will be visible to the other players.
     * If you are planning to make it web2 server, consider that some data shouldn't be known by the other player.
     */

    /*
     * public int playerChampion;
     * public int playerChampionLevel; Not implemented yet
     * public int[] items = new int[5];
     * public int[] runes = new int[5];
     * Non-Primitive data types cannot be serialized here, they have to be serialized inside each class.
     * Why should we do this while we can save the ids of them?
     * This is an interesting question. But we shouldn't just save their id's to the web3 contract.
     * For now, we will save the structs of them.
     */

    /*
     * Calling updater functions continously is not ideal.
     * When there is a change in items, runes or champion this functions should be called to update player data.
     * Although this functions are simple, this part will belong to the server side that is connected to web3 as validator.
     * That's why speed is everything.
     */
    public PlayerTeam playerTeam;
    public PlayerAnimationState playerAnimationState;
    public NetworkChampion playerChampion;
    //public NetworkItem[] items;
    //public NetworkRune[] runes;
    public Vector2 playerMovementDestination;
    public float playerMovementTime;
    public float playerLastAttackTime;
    public float playerTotalHealth;
    public float playerHealthRegenerationSpeed;
    public float playerHealthStoleMultiplier;
    public float playerHealth;
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
    public bool isSet, isMoveRequested, isMoving;

    public PlayerData(NetworkChampion networkChampion, PlayerTeam team)
    {
        isSet = isMoveRequested = isMoving = false;
        playerMovementDestination = Vector2.zero;
        playerMovementTime = 0f;
        playerLastAttackTime = 0f;
        playerTeam = team;
        playerAnimationState = PlayerAnimationState.Idle;
        playerChampion = networkChampion;
        //items = new NetworkItem[5]; for (int i = 0; i < items.Length; i++) items[i] = new NetworkItem();
        //runes = new NetworkRune[5]; for (int i = 0; i < runes.Length; i++) runes[i] = new NetworkRune();
        playerTotalHealth = playerHealthRegenerationSpeed = playerHealthStoleMultiplier = playerHealth = 0f;
        playerTotalMana = playerManaRegenerationSpeed = playerMana = 0f;
        playerAttackCooldownTime = playerADAttackDamage = playerAPAttackDamage = 0f;
        playerADArmor = playerAPArmor = playerADArmorPiercing = playerAPArmorPiercing = 0f;
        playerMovementSpeed = 0f;
        isSet = true;
    }

    public void UpdatePlayerData()
    {
        /*
        playerTotalHealth = UpdateTotalHealth();
        playerHealthRegenerationSpeed = UpdateHealthRegenerationSpeed();
        playerHealthStoleMultiplier = UpdateHealthStoleMultiplier();
        playerTotalMana = UpdateTotalMana();
        playerManaRegenerationSpeed = UpdateManaRegenerationSpeed();
        playerAttackCooldownTime = UpdateAttackCooldownTime();
        playerADAttackDamage = UpdateADAttackDamage();
        playerAPAttackDamage = UpdateAPAttackDamage();
        playerADArmor = UpdateADArmor();
        playerAPArmor = UpdateAPArmor();
        playerADArmorPiercing = UpdateADArmorPiercing();
        playerAPArmorPiercing = UpdateAPArmorPiercing();
        playerMovementSpeed = UpdateMovementSpeed();
        */
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref isSet);
        serializer.SerializeValue(ref isMoveRequested);
        serializer.SerializeValue(ref isMoving);
        serializer.SerializeValue(ref playerAnimationState);
        serializer.SerializeValue(ref playerMovementDestination);
        serializer.SerializeValue(ref playerMovementTime);
        serializer.SerializeValue(ref playerLastAttackTime);
        serializer.SerializeValue(ref playerTeam);
        serializer.SerializeValue(ref playerTotalHealth);
        serializer.SerializeValue(ref playerHealthRegenerationSpeed);
        serializer.SerializeValue(ref playerHealthStoleMultiplier);
        serializer.SerializeValue(ref playerHealth);
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
        playerChampion.NetworkSerialize(serializer);
        //items = new NetworkItem[5]; for (int i = 0; i < items.Length; i++) items[i] = new NetworkItem();
        //runes = new NetworkRune[5]; for (int i = 0; i < runes.Length; i++) runes[i] = new NetworkRune();
        //foreach (NetworkItem item in items) item.NetworkSerialize(serializer);
        //
        //foreach (NetworkRune rune in runes) rune.NetworkSerialize(serializer);
    }

    /*
    private float UpdateTotalHealth() => playerTotalHealth = playerChampion.totalHealth + SumExtraTotalHealth();
    private float UpdateHealthRegenerationSpeed() => playerHealthRegenerationSpeed = playerChampion.healthRegenerationSpeed + (playerChampion.healthRegenerationSpeed * SumExtraTotalHealthRegenerationSpeed() / 100);
    private float UpdateHealthStoleMultiplier() => playerHealthStoleMultiplier = playerChampion.healthStoleMultiplier + SumExtraHealthStoleMultiplier();
    private float UpdateTotalMana() => playerTotalMana = playerChampion.totalMana + SumExtraTotalMana();
    private float UpdateManaRegenerationSpeed() => playerManaRegenerationSpeed = playerChampion.manaRegenerationSpeed + (playerChampion.manaRegenerationSpeed * SumExtraManaRegenerationSpeed() / 100);
    private float UpdateAttackCooldownTime() => playerAttackCooldownTime = playerChampion.attackCooldownTime - (playerChampion.attackCooldownTime * SumExtraAttackSpeed() / 100);
    private float UpdateADAttackDamage() => playerADAttackDamage = playerChampion.adAttackDamage + (playerChampion.adAttackDamage * SumExtraADAttackDamage() / 100);
    private float UpdateAPAttackDamage() => playerAPAttackDamage = playerChampion.apAttackDamage + (playerChampion.apAttackDamage * SumExtraAPAttackDamage() / 100);
    private float UpdateADArmor() => playerADArmor = playerChampion.adArmor + SumExtraADArmor();
    private float UpdateAPArmor() => playerAPArmor = playerChampion.apArmor + SumExtraAPArmor();
    private float UpdateADArmorPiercing() => playerADArmorPiercing = playerChampion.adArmorPiercing + (playerChampion.adArmorPiercing * SumExtraADArmorPiercing() / 100);
    private float UpdateAPArmorPiercing() => playerAPArmorPiercing = playerChampion.apArmorPiercing + (playerChampion.apArmorPiercing * SumExtraAPArmorPiercing() / 100);
    private float UpdateMovementSpeed() => playerMovementSpeed = playerChampion.movementSpeed + (playerChampion.movementSpeed * SumExtraMovementSpeed() / 100);
    private float SumExtraTotalHealth() => items.Sum(item => item.extraTotalHealth) + runes.Sum(rune => rune.extraTotalHealth);
    private float SumExtraTotalHealthRegenerationSpeed() => items.Sum(item => item.extraHealthRegenerationSpeed) + runes.Sum(rune => rune.extraHealthRegenerationSpeed);
    private float SumExtraHealthStoleMultiplier() => items.Sum(item => item.extraHealthStoleMultiplier) + runes.Sum(rune => rune.extraHealthStoleMultiplier);
    private float SumExtraTotalMana() => items.Sum(item => item.extraTotalMana) + runes.Sum(rune => rune.extraTotalMana);
    private float SumExtraManaRegenerationSpeed() => items.Sum(item => item.extraManaRegenerationSpeed) + runes.Sum(rune => rune.extraManaRegenerationSpeed);
    private float SumExtraAttackSpeed() => items.Sum(item => item.extraAttackSpeed) + runes.Sum(rune => rune.extraAttackSpeed);
    private float SumExtraADAttackDamage() => items.Sum(item => item.extraADAttackDamage) + runes.Sum(rune => rune.extraADAttackDamage);
    private float SumExtraAPAttackDamage() => items.Sum(item => item.extraAPAttackDamage) + runes.Sum(rune => rune.extraAPAttackDamage);
    private float SumExtraADArmor() => items.Sum(item => item.extraADArmor) + runes.Sum(rune => rune.extraADArmor);
    private float SumExtraAPArmor() => items.Sum(item => item.extraAPArmor) + runes.Sum(rune => rune.extraAPArmor);
    private float SumExtraADArmorPiercing() => items.Sum(item => item.extraADArmorPiercing) + runes.Sum(rune => rune.extraADArmorPiercing);
    private float SumExtraAPArmorPiercing() => items.Sum(item => item.extraAPArmorPiercing) + runes.Sum(rune => rune.extraAPArmorPiercing);
    private float SumExtraMovementSpeed() => items.Sum(item => item.extraMovementSpeed) + runes.Sum(rune => rune.extraMovementSpeed);*/

    public PlayerData GeneratePlayerData(Vector2 newMovementDestination, float newMovementTime) => new PlayerData()
    {
        isSet = isSet,
        isMoveRequested = true,
        isMoving = true,
        playerMovementDestination = newMovementDestination,
        playerMovementTime = newMovementTime,
        playerLastAttackTime = playerLastAttackTime,
        playerTeam = playerTeam,
        playerAnimationState = playerAnimationState,
        playerChampion = playerChampion,
        //items = items,
        //runes = runes,
        playerTotalHealth = playerTotalHealth,
        playerHealthRegenerationSpeed = playerHealthRegenerationSpeed,
        playerHealthStoleMultiplier = playerHealthStoleMultiplier,
        playerHealth = playerHealth,
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

    public PlayerData GeneratePlayerData(string boolName, bool newValue) => new PlayerData()
    {
        isSet = isSet,
        isMoveRequested = boolName == "isMoveRequested" ? newValue : isMoveRequested,
        isMoving = boolName == "isMoving" ? newValue : isMoving,
        playerMovementDestination = playerMovementDestination,
        playerMovementTime = playerMovementTime,
        playerLastAttackTime = playerLastAttackTime,
        playerTeam = playerTeam,
        playerAnimationState = playerAnimationState,
        playerChampion = playerChampion,
        //items = items,
        //runes = runes,
        playerTotalHealth = playerTotalHealth,
        playerHealthRegenerationSpeed = playerHealthRegenerationSpeed,
        playerHealthStoleMultiplier = playerHealthStoleMultiplier,
        playerHealth = playerHealth,
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

    public PlayerData GeneratePlayerData(PlayerAnimationState newAnimationState) => new PlayerData()
    {
        isSet = isSet,
        isMoveRequested = isMoveRequested,
        isMoving = isMoving,
        playerMovementDestination = playerMovementDestination,
        playerMovementTime = playerMovementTime,
        playerLastAttackTime = playerLastAttackTime,
        playerTeam = playerTeam,
        playerAnimationState = newAnimationState,
        playerChampion = playerChampion,
        //items = items,
        //runes = runes,
        playerTotalHealth = playerTotalHealth,
        playerHealthRegenerationSpeed = playerHealthRegenerationSpeed,
        playerHealthStoleMultiplier = playerHealthStoleMultiplier,
        playerHealth = playerHealth,
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

    /*
    public static implicit operator PlayerData(Player player) => new PlayerData() {
        isSet = player.tempPlayerData.isSet,
        playerMovementDestination = player.tempPlayerData.playerMovementDestination,
        playerMovementTime = player.tempPlayerData.playerMovementTime,
        playerLastAttackTime = player.tempPlayerData.playerLastAttackTime,
        playerTeam = player.tempPlayerData.playerTeam,
        playerChampion = player.tempPlayerData.playerChampion,
        items = player.tempPlayerData.items,
        runes = player.tempPlayerData.runes,
        playerTotalHealth = player.tempPlayerData.playerTotalHealth,
        playerHealthRegenerationSpeed = player.tempPlayerData.playerHealthRegenerationSpeed,
        playerHealthStoleMultiplier = player.tempPlayerData.playerHealthStoleMultiplier,
        playerHealth = player.tempPlayerData.playerHealth,
        playerTotalMana = player.tempPlayerData.playerTotalMana,
        playerManaRegenerationSpeed = player.tempPlayerData.playerManaRegenerationSpeed,
        playerMana = player.tempPlayerData.playerMana,
        playerAttackCooldownTime = player.tempPlayerData.playerAttackCooldownTime,
        playerADAttackDamage = player.tempPlayerData.playerADAttackDamage,
        playerAPAttackDamage = player.tempPlayerData.playerAPAttackDamage,
        playerADArmor = player.tempPlayerData.playerADArmor,
        playerAPArmor = player.tempPlayerData.playerAPArmor,
        playerADArmorPiercing = player.tempPlayerData.playerADArmorPiercing,
        playerAPArmorPiercing = player.tempPlayerData.playerAPArmorPiercing,
        playerMovementSpeed = player.tempPlayerData.playerMovementSpeed,
    };
    */
}