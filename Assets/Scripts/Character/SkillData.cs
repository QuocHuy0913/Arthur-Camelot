using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Game/SkillData")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public string description;
    public int mpCost;
    public int damage;
    public Sprite icon;
}
