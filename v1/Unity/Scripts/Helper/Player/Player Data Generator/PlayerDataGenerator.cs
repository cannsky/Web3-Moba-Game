using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataGenerator
{
    public static PlayerChampionData GeneratePlayerChampionData(int id)
    {
        Champion champion = (Champion) PlayerResourceFinder.Find(PlayerResourceFinder.Type.Champion, id);
        return new PlayerChampionData()
        {
            ID = champion.ID,
            championType = (PlayerChampionData.ChampionType) champion.championType,
            damageType = (PlayerChampionData.DamageType) champion.damageType,
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
