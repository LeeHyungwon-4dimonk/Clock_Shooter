using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("OutArea") || collision.gameObject.CompareTag("Monster"))
        {
            Manager.Pool.Return("Bullet", gameObject);
        }
    }
}