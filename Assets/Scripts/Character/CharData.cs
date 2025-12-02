using UnityEngine;

[CreateAssetMenu(fileName = "CharData", menuName = "Game/CharData")]
public class CharData : ScriptableObject
{
    public string charName;
    public Sprite avatar;
    public int hp;
    public int mp;
    public int basicDamage;

    [Header("Movement sprites (optional)")]
    public AnimationClip IdleUp;
    public AnimationClip IdleDown;
    public AnimationClip IdleLeft;
    public AnimationClip IdleRight;
    public AnimationClip walkUp;
    public AnimationClip walkDown;
    public AnimationClip walkLeft;
    public AnimationClip walkRight;

    [Header("Skills (optional)")]
    public SkillData[] skills;
}
