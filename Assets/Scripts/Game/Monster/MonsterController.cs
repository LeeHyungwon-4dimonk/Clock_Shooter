using DG.Tweening;
using System;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public static event Action OnMonsterDestroyed;
    public int DirectionIndex { get; private set; }
    public int DistanceStep { get; private set; }

    private float _stepDistance = 2.5f;
    private float _spawnRadius = 13f;
    private float _yOffset = 1f;
    private int _directionCount = 8;

    private float _moveDuration = 0.2f;

    public void Initialize(int directionIndex)
    {
        DirectionIndex = directionIndex;
        DistanceStep = 0;
        UpdatePosition();
    }

    private void OnEnable()
    {
        Manager.Game.turnStack.OnChanged += Move;
    }

    private void OnDisable()
    {
        Manager.Game.turnStack.OnChanged -= Move;
    }

    private void Move(int prev, int cur)
    {
        if (cur - prev < 0) MoveBackward();
        else if(cur - prev > 0) MoveForward();
    }

    public void MoveForward()
    {
        DistanceStep++;
        UpdatePosition();
    }

    public void MoveBackward()
    {
        DistanceStep = Mathf.Max(0, DistanceStep - 1);
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        transform.DOKill();

        float angle = DirectionIndex * (360f / _directionCount);
        float rad = angle * Mathf.Deg2Rad;

        float radius = _spawnRadius - (DistanceStep * _stepDistance);

        Vector3 targetPos = new Vector3(Mathf.Cos(rad) * radius, _yOffset, Mathf.Sin(rad) * radius);
        transform.DOLocalMove(targetPos, _moveDuration).SetEase(Ease.OutQuad);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        OnMonsterDestroyed?.Invoke();
        Manager.Pool.Return("Monster", gameObject);
    }
}