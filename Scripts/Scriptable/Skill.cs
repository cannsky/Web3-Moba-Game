using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class Skill : ScriptableObject
{
    public enum SkillType { AD, AP }

    public SkillType skillType;
    public AnimationClip animation;
    public GameObject vfx;
    public AudioClip sound;
    public float radius;
    public float range;
    public float attackDamage;
    public float armor;
    public float startTime;
    public float endTime;
}