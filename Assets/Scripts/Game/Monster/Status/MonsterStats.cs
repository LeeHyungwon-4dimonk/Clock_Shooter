using UnityEngine;

public class MonsterStats : Stats
{
    public int Exp {  get; private set; }

    public MonsterStats(MonsterStatData data)
    {
        InitiailizeHP(data.MaxHP);
        Exp = data.Exp;
    }
}