using System;
using UnityEditor.Analytics;

public class PlayerStats : Stats
{    
    public int AttackDamage { get; private set; }
    public int Level { get; private set; }
    public int Exp { get; private set; }
    public int[] RequiredExp { get; private set; }

    public PlayerStats(PlayerStatData data)
    {
        InitiailizeHP(data.MaxHP);
        AttackDamage = data.AttackDamage;
        Level = data.StartLevel;
        RequiredExp = data.RequiredExp;
    }

    public void AddExperience(int exp)
    {
        Exp += exp;
        if(Exp >= RequiredExp[Level - 1])
        {
            LevelUp();
        }

        NotifyStatusChanged();
    }

    private void LevelUp()
    {
        Exp -= RequiredExp[Level - 1];
        Level++;

        // ½ºÅÝ ¹ë·±½ÌÀº ÃßÈÄ¿¡
        AttackDamage += 1;
    }
}