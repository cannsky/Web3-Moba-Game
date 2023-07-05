using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public enum ItemType { AD, AP }

    public ItemType itemType;
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