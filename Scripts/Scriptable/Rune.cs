using UnityEngine;

[CreateAssetMenu(fileName = "New Rune", menuName = "Rune")]
public class Rune : ScriptableObject
{
    public enum RuneType { AD, AP }

    public RuneType runeType;
    public float extraTotalHealth;
    public float extraHealthRegenerationSpeed;
    public float extraHealthStoleMultiplier;
    public float extraTotalMana;
    public float extraManaRegenerationSpeed;
    public float extraAttackSpeed;
    public float extraAttackDamage;
    public float extraArmor;
    public float extraArmorPiercing;
    public float extraMovementSpeed;
}