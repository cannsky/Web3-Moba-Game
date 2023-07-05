using UnityEditor.Animations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "New Champion", menuName = "Champion")]
public class Champion : ScriptableObject
{
    public AnimatorController animatorController;
    public enum ChampionType { Melee, Ranged }
    public enum DamageType { AD, AP }

    public ChampionType championType;
    public DamageType damageType;

    public float range;
    public GameObject characterPrefab;
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
    public float armorPiercingAD;
    public float armorPiercingAP;
    public float movementSpeed;

    public Skill qSkill;
    public Skill wSkill;
    public Skill eSkill;
    public Skill rSkill;
}