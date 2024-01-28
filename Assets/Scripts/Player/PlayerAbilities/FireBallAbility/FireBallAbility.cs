using UnityEngine;
using System.Collections;

public class FireBallAbility : PlayerAbility
{
    [Header("Ability Settings")]
    [SerializeField] private float launchOffset;
    [SerializeField] private float startShootCooldown;
    [SerializeField] private int outputDamage;
    [SerializeField] private float knockBackForce;

    private float shootCooldown = 0;

    [Header("FireBall Prefab")]
    [SerializeField] private GameObject fireBallPrefab;

    public override void Activate()
    {
        if (shootCooldown < Time.time)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 direction = PlayerController.lastDirection;
        StartCoroutine(Knockback(direction * -1f, 9, .1f));

        GameObject fireBall = Instantiate(fireBallPrefab, transform.position + launchOffset * direction + new Vector3(0, 3, 0), Quaternion.identity);
        fireBall.GetComponent<Fireball>().SetUpFireBall(outputDamage, knockBackForce, direction, PlayerController);

        PlayerController.animator.SetTrigger("MagicAttack");

        shootCooldown = startShootCooldown + Time.time;
    }

    private IEnumerator Knockback(Vector2 force, float kockbackForce, float knockBackduration, bool leaveMovementOff = false)
    {
        PlayerController.CanMove = false;

        yield return null;

        RigidBody.AddForce(force.normalized * kockbackForce, ForceMode.Impulse);

        yield return new WaitForSeconds(knockBackduration);

        if (leaveMovementOff)
        {
            RigidBody.velocity = Vector2.zero;
            yield break;
        }

        PlayerController.CanMove = true;
    }
}
