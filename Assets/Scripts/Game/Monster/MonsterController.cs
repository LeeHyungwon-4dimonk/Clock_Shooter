using DG.Tweening;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] float moveStep = 0.8f;
    [SerializeField] float moveDuration = 0.2f;

    private void OnEnable()
    {
        Manager.Game.turnStack.OnChanged += MoveRadius;
    }

    private void OnDisable()
    {
        Manager.Game.turnStack.OnChanged -= MoveRadius;
    }

    public void MoveRadius(int prev, int cur)
    {
        transform.DOKill();

        Vector3 localPos = transform.localPosition;
        float radius = localPos.magnitude;

        radius += (moveStep * -(cur - prev));

        Vector3 targetPos = localPos.normalized * radius;

        transform.DOLocalMove(targetPos, moveDuration).SetEase(Ease.OutQuad);
    }
}