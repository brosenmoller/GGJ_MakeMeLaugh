using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAreaHeal : PlayerAbility
{
    [SerializeField] GameObject healZoneObj;
    [SerializeField] float yOfset;

    [SerializeField] float healAmount = 1;
    [SerializeField] float timeBetweenHeal = 0.1f;
    [SerializeField] float totalHealFieldTime;
    [SerializeField] float abilityCooldown = 1;
    bool abilityActive = true;
    public override void Activate()
    {
        //Debug.Log("A HOPELESS ROMANTIC ALL MY LIFE");
        if (abilityActive)
        {
            abilityActive = false;
            new Timer(abilityCooldown, () => BenGaatDoodDoorHongerigeEgels());
            var spawnObj = Instantiate(healZoneObj);
            spawnObj.transform.position = new Vector3(transform.position.x, transform.position.y + yOfset, transform.position.z);
            spawnObj.GetComponent<Healzone>().startHeal(healAmount,totalHealFieldTime,timeBetweenHeal);
        }
        else
        {
            //Fail audio ofzo?
        }
    }
    private void BenGaatDoodDoorHongerigeEgels()
    {
        abilityActive = true;
    }
}
