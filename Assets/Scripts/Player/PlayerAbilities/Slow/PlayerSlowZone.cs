using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSlowZone : PlayerAbility
{
    [Header("Assign dit BITCH")]
    [SerializeField] private GameObject slowZoneObj;
    [Header("SlowSettings")]
    [SerializeField] private float slowFactor = 0.6f;
    [SerializeField] private float slowTime = 5;
    [Tooltip("DE CODE WERKT WEER LETGOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO")]// Het commend hier onder is obeselete 😎
    [SerializeField] private float abilityCooldown = 6f; // <--- ik realiseer me nu dat deze code breekt als slowtime hoger is dan ability cooldown.
    [SerializeField] private float yOfset = 2.3f;
    private bool abilityActive = true;
    public override void Activate()
    {
        //Debug.Log("A HOPELESS ROMANTIC ALL MY LIFE");
        if (abilityActive) 
        {
            abilityActive = false;
            new Timer(abilityCooldown, () => BenGaatDoodDoorHongerigeEgels());
            var spawnObj = Instantiate(slowZoneObj);
            spawnObj.transform.position = new Vector3(transform.position.x, transform.position.y + yOfset, transform.position.z);
            spawnObj.GetComponent<SlowZone>().SetSlowVar(slowFactor, slowTime);
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
