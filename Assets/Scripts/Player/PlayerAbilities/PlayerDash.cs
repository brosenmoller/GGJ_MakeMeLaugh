using System.Collections;
using UnityEngine;

public class PlayerDash : PlayerAbility
{
    [Header("Dash Settings")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashActiveTime;
    [SerializeField] private float dashCooldownTime;

    private bool canDash = true;

    public override void Activate()
    {
        if (canDash)
        {
            StartCoroutine(Dashing());
        }
    }

    private IEnumerator Dashing()
    {
        RigidBody.velocity = PlayerController.lastDirection * dashForce;
        PlayerController.CanMove = false;
        canDash = false;

        yield return new WaitForSeconds(dashActiveTime);

        PlayerController.CanMove = true;

        yield return new WaitForSeconds(dashCooldownTime);

        canDash = true;
    }
}

