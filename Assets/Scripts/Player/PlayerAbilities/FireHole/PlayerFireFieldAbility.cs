using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireFieldAbility : PlayerAbility
{
    [Header("Assign dit BITCH")]
    [SerializeField] private GameObject fireZoneObj;
    [SerializeField] private PlayerController playerController;

    [Header("SlowSettings")]
    [SerializeField] float fireDamge = 0.2f;
    [SerializeField] float burningtime = 5f;
    [SerializeField] float amoundOfBurns = 5f;
    [SerializeField] float circelDespawnTime = 5f;

    [SerializeField] private float abilityCooldown = 6f;
    [SerializeField] private float yOfset = 4.32f;
    private bool abilityActive = true;
    public override void Activate()
    {
        //Debug.Log("A HOPELESS ROMANTIC ALL MY LIFE");
        if (abilityActive)
        {
            abilityActive = false;
            new Timer(abilityCooldown, () => BenGaatDoodDoorHongerigeEgels());
            var spawnObj = Instantiate(fireZoneObj);
            spawnObj.transform.position = new Vector3(transform.position.x, transform.position.y + yOfset, transform.position.z);
            spawnObj.GetComponent<FireZone>().StartFire(circelDespawnTime, burningtime, fireDamge, amoundOfBurns,playerController);
            PlayerController.animator.SetTrigger("MagicAreaAttack");
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
