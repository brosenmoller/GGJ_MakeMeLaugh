using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : PlayerAbility
{
    [SerializeField] private float damgeTakenMultipier = 0.5f;
    [SerializeField] private float shieldDuration = 4;
    [SerializeField] private float abilityCooldown = 6;
    [SerializeField] private PlayerController controller;
    private bool abilityActive = true;

    public override void Activate()
    {
        if (abilityActive)
        {
            abilityActive = false;
            new Timer(abilityCooldown, () => BenGaatDoodDoorHongerigeEgels());
            controller.ChangeDamageTakeMultiplier(damgeTakenMultipier);
            new Timer(shieldDuration, () => DeactivateShield());
        }
    }

    private void DeactivateShield() 
    { 
        controller.ChangeDamageTakeMultiplier(1);
    }

    private void BenGaatDoodDoorHongerigeEgels()
    {
        abilityActive = true;
    }
}
