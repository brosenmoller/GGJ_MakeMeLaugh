using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [Header("Stats")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float getHitKnockbackMultiplier = 1.0f;

    [Header("References")]
    [SerializeField] private Material whiteFlash;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;

    private float health;
    private Material normalMaterial;
    private Rigidbody rigidBody;

    private void Awake()
    {
        health = maxHealth;
        normalMaterial = meshRenderer.material;

        rigidBody = GetComponent<Rigidbody>();
    }

    public void TakeDamage(float damage, PlayerController player, float knockBackForce, Vector3 launchDirection)
    {
        health -= damage;

        meshRenderer.material = whiteFlash;

        new Timer(0.2f, () => { meshRenderer.material = normalMaterial; });

        rigidBody.AddForce(getHitKnockbackMultiplier * knockBackForce * launchDirection.normalized, ForceMode.Impulse);

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

