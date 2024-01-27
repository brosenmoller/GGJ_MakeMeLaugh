using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Material whiteFlash;
    [SerializeField] private MeshRenderer meshRenderer;

    private float health;
    private Material normalMaterial;

    private void Awake()
    {
        health = maxHealth;
        normalMaterial = meshRenderer.material;
    }

    public void TakeDamage(float damage, PlayerController player)
    {
        health -= damage;

        meshRenderer.material = whiteFlash;
        new Timer(0.2f, () => { meshRenderer.material = normalMaterial; });

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

