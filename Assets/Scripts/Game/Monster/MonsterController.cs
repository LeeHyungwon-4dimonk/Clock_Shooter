using DG.Tweening;
using System;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] int moveTurn = 8;
    [SerializeField] float moveDuration = 0.2f;

    private float[] vectors = new float[2];

    public static event Action OnMonsterDestroyed;

    private void Start()
    {
        vectors[0] = transform.localPosition.x / moveTurn;
        vectors[1] = transform.localPosition.z / moveTurn;
    }

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

        Vector3 curPos = transform.localPosition;

        int dir = cur - prev;

        Vector3 targetPos = new Vector3(curPos.x - vectors[0] * dir,
            curPos.y, curPos.z - vectors[1] * dir);

        transform.DOLocalMove(targetPos, moveDuration).SetEase(Ease.OutQuad);
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        OnMonsterDestroyed?.Invoke();
        Manager.Pool.Return("Monster", gameObject);
    }
}