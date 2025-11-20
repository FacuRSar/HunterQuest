using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage = 10;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            SlimeHealthController enemyHealth = other.GetComponent<SlimeHealthController>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}

