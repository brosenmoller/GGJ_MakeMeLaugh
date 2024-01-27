using UnityEngine;

public interface IDamagable
{
    void TakeDamage(float damages, PlayerController player, float knockBackForce, Vector3 launchDirection);
}

