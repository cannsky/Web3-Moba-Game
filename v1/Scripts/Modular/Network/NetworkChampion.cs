using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[System.Serializable]
public struct NetworkChampion : INetworkSerializable
{
    public enum ChampionType { Melee, Ranged }
    public enum DamageType { AD, AP }

    public int ID;
    //public NetworkSkill[] skills;
    public ChampionType championType;
    public DamageType damageType;
    public float range;
    public float attackCooldownTime;
    public float adAttackDamage;
    public float apAttackDamage;
    public float totalHealth;
    public float healthRegenerationSpeed;
    public float healthStoleMultiplier;
    public float totalMana;
    public float manaRegenerationSpeed;
    public float adArmor;
    public float apArmor;
    public float adArmorPiercing;
    public float apArmorPiercing;
    public float movementSpeed;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        //skills = new NetworkSkill[4];
        serializer.SerializeValue(ref ID);
        serializer.SerializeValue(ref championType);
        serializer.SerializeValue(ref damageType);
        serializer.SerializeValue(ref range);
        serializer.SerializeValue(ref attackCooldownTime);
        serializer.SerializeValue(ref adAttackDamage);
        serializer.SerializeValue(ref apAttackDamage);
        serializer.SerializeValue(ref totalHealth);
        serializer.SerializeValue(ref healthRegenerationSpeed);
        serializer.SerializeValue(ref healthStoleMultiplier);
        serializer.SerializeValue(ref totalMana);
        serializer.SerializeValue(ref manaRegenerationSpeed);
        serializer.SerializeValue(ref adArmor);
        serializer.SerializeValue(ref apArmor);
        serializer.SerializeValue(ref adArmorPiercing);
        serializer.SerializeValue(ref apArmorPiercing);
        serializer.SerializeValue(ref movementSpeed);
        //foreach (var skill in skills) skill.NetworkSerialize(serializer);
    }
}
