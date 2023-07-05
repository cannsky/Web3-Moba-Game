using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class Attack : ScriptableObject
{
    public AnimationClip attackAnimation;
    public GameObject attackVFX;
    public AudioClip attackSound;
}