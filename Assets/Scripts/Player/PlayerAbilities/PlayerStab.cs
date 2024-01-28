using UnityEngine;

public class PlayerStab : PlayerAbility
{
    [Header("Attack Settings")]
    [SerializeField] private int baseDamage;
    [SerializeField] private float startAttackCooldown;
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackOffset;
    [SerializeField] private float knockBackForce;

    private float attackCooldown;
    //public Animator animator;

    public override void Activate()
    {
        if (attackCooldown <= Time.time)
        {
            //Debug.Log("Attack");
            attackCooldown = startAttackCooldown + Time.time;
            //animator.SetBool("Attack", true);

            Collider[] colliders = Physics.OverlapSphere(transform.position + (PlayerController.lastDirection * attackOffset), attackRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out IDamagable damagable))
                {
                    damagable.TakeDamage(baseDamage, PlayerController, knockBackForce, PlayerController.lastDirection, Color.white);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3.left * attackOffset), attackRadius);
    }
}

