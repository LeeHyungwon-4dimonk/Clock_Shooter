using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("OutArea"))
        {
            Manager.Pool.Return("Bullet", gameObject);
        }
        else if(collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.TryGetComponent(out Monster monster);
            Manager.Status.ApplyPlayerAttack(monster);
            Manager.Pool.Return("Bullet", gameObject);
        }
    }
}