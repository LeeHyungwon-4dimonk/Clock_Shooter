using DG.Tweening;
using System;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public static event Action OnMonsterDestroyed;

    public int DirectionIndex { get; private set; }
    public int DistanceStep { get; private set; }

    [SerializeField] private float _spawnRadius = 13f;
    [SerializeField] private float _stepDistance = 2.5f;
    [SerializeField] private float _yOffset = 1f;
    [SerializeField] private float _moveDuration = 0.2f;

    /* ---------- 초기화 ---------- */

    public void Initialize(int directionIndex)
    {
        DirectionIndex = directionIndex;
        DistanceStep = 0;

        Manager.Game.monsterPositionManager.Register(this);
        UpdatePositionImmediate();
    }

    /* ---------- 이동 판단 ---------- */

    public int GetTargetStep(int delta)
    {
        if (delta > 0) return DistanceStep + 1;
        if (delta < 0) return Mathf.Max(0, DistanceStep - 1);
        return DistanceStep;
    }

    /* ---------- 이동 적용 ---------- */

    public void ApplyMove(int targetStep)
    {
        DistanceStep = targetStep;
        UpdatePosition();
    }

    private void UpdatePositionImmediate()
    {
        transform.localPosition = CalculatePosition();
    }

    private void UpdatePosition()
    {
        transform.DOKill();
        transform.DOLocalMove(CalculatePosition(), _moveDuration)
            .SetEase(Ease.OutQuad);
    }

    private Vector3 CalculatePosition()
    {
        float angle = DirectionIndex * (360f / 8f) * Mathf.Deg2Rad;
        float radius = _spawnRadius - (DistanceStep * _stepDistance);

        return new Vector3(
            Mathf.Cos(angle) * radius,
            _yOffset,
            Mathf.Sin(angle) * radius
        );
    }

    /* ---------- 파괴 ---------- */

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Manager.Game.monsterPositionManager.Unregister(this);
        OnMonsterDestroyed?.Invoke();
        Manager.Pool.Return("Monster", gameObject);
    }
}