using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private MonsterStatData statData;

    public MonsterStats Stats { get; private set; }

    private void Awake()
    {
        Stats = new MonsterStats(statData);
    }
}