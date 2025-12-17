using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatData", menuName ="Stats/Player")]
public class PlayerStatData : ScriptableObject
{
    public int MaxHP;
    public int AttackDamage;
    public int StartLevel;
    public int[] RequiredExp;
}