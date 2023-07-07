using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassToStruct
{
    public static NetworkChampion ConvertChampion(int id)
    {
        Champion champion = (Champion) ResourceFinder.Find(ResourceFinder.Type.Champion, id);
        return new NetworkChampion()
        {
            ID = champion.ID,
            championType = (NetworkChampion.ChampionType) champion.championType,
            damageType = (NetworkChampion.DamageType) champion.damageType,
            range = champion.range,
            attackCooldownTime = champion.attackCooldownTime,
            adAttackDamage = champion.adAttackDamage,
            apAttackDamage = champion.apAttackDamage,
            totalHealth = champion.totalHealth,
            healthRegenerationSpeed = champion.healthRegenerationSpeed,
            healthStoleMultiplier = champion.healthStoleMultiplier,
            totalMana = champion.totalMana,
            manaRegenerationSpeed = champion.manaRegenerationSpeed,
            adArmor = champion.adArmor,
            apArmor = champion.apArmor,
            adArmorPiercing = champion.adArmorPiercing,
            apArmorPiercing = champion.apArmorPiercing,
            movementSpeed = champion.movementSpeed
        };
    }
}
