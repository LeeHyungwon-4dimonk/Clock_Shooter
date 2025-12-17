using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStatData statData;

    public PlayerStats Stats { get; private set; }

    private void Awake()
    {
        Stats = new PlayerStats(statData);
    }
}