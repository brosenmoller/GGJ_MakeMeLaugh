using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : PlayerAbility
{
    [Header("Assign dit BITCH")]
    [SerializeField] private GameObject stunZoneObj;
    [SerializeField] private float stunTime = 5;
    [SerializeField] private float stunDamage = 1;
    [SerializeField] private float abilityCooldown = 6f;
    [SerializeField] private float yOfset = 2.3f;
    [SerializeField] private PlayerController player;
    private bool abilityActive = true;
    public override void Activate()
    {
        //Debug.Log("A HOPELESS ROMANTIC ALL MY LIFE");
        if (abilityActive)
        {
            abilityActive = false;
            new Timer(abilityCooldown, () => BenGaatDoodDoorHongerigeEgels());
            var spawnObj = Instantiate(stunZoneObj);
            spawnObj.transform.position = new Vector3(transform.position.x, transform.position.y + yOfset, transform.position.z);
            spawnObj.GetComponent<StunZone>().Instanciate(stunTime,stunDamage,player);
        }
        else
        {
            //Fail audio ofzo?
        }
        //seks? sure
    }

    private void BenGaatDoodDoorHongerigeEgels()
    {
        abilityActive = true;
    }

}
