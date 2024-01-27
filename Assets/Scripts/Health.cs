using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth;

    private float health;

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage, Player player)
    {
        health -= damage;

        if (health < 0)
        {
            player.GiveXp(0.01f);
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}

