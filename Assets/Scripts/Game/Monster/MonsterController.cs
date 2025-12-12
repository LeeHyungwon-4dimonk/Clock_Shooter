using DG.Tweening;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] float moveStep = 0.4f;
    [SerializeField] float moveDuration = 0.3f;

    private void OnEnable()
    {
        Manager.Game.OnTurnStackChanged+= MoveRadius;
    }

    private void OnDisable()
    {
        Manager.Game.OnTurnStackChanged -= MoveRadius;
    }

    public void MoveRadius(int dir)
    {
        transform.DOKill();

        Vector3 localPos = transform.localPosition;
        float radius = localPos.magnitude;

        radius += (moveStep * -dir);

        Vector3 targetPos = localPos.normalized * radius;

        transform.DOLocalMove(targetPos, moveDuration).SetEase(Ease.OutQuad);
    }
}