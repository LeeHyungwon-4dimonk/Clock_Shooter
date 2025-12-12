using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("OutArea"))
        {
            Manager.Pool.Return("Bullet", this.gameObject);
        }
    }
}