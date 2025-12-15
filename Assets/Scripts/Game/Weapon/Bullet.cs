using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("OutArea"))
        {
            Manager.Pool.Return("Bullet", this.gameObject);
        }
        else if(collision.gameObject.CompareTag("Monster"))
        {
            Manager.Pool.Return("Monster", collision.gameObject);
            Manager.Pool.Return("Bullet", this.gameObject);
        }        
    }
}