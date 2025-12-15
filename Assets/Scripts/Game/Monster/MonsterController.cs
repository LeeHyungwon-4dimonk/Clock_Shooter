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

        Manager.Game.monsterPositionManager.Register(this, DirectionIndex, DistanceStep);
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
        int targetStep = DistanceStep + 1;

        if (!Manager.Game.monsterPositionManager.TryMove(this, targetStep)) return;

        DistanceStep = targetStep;
        UpdatePosition();
    }

    public void MoveBackward()
    {
        int targetStep = DistanceStep - 1;

        if (!Manager.Game.monsterPositionManager.TryMove(this, targetStep)) return;

        DistanceStep = targetStep;
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

        Manager.Game.monsterPositionManager.Unregister(DirectionIndex, DistanceStep);

        OnMonsterDestroyed?.Invoke();
        Manager.Pool.Return("Monster", gameObject);
    }
}