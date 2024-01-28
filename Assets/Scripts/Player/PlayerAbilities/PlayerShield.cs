using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : PlayerAbility
{
    [SerializeField] private float damgeTakenMultipier = 0.5f;
    [SerializeField] private float shieldDuration = 4;
    [SerializeField] private float abilityCooldown = 6;
    [SerializeField] private PlayerController controller;
    [SerializeField] private GameObject shieldParticle;
    [SerializeField] private float particleYOfset;
    private bool abilityActive = true;

    public override void Activate()
    {
        if (abilityActive)
        {
            abilityActive = false;
            new Timer(abilityCooldown, () => BenGaatDoodDoorHongerigeEgels());
            controller.ChangeDamageTakeMultiplier(damgeTakenMultipier);
            var temp = Instantiate(shieldParticle);
            temp.transform.SetParent(transform);
            temp.transform.position = new Vector3(transform.position.x,transform.position.y + particleYOfset,transform.position.z);
            Destroy(temp,shieldDuration);
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
