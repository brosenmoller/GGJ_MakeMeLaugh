using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelUp : PlayerAbility
{

    [SerializeField] private float abilityCooldown;
    [SerializeField] private float levelRange = 8;
    [SerializeField] GameObject lvlParticle;
    [SerializeField] float particleYOfset;
    private bool abilityActive = true;
    public override void Activate()
    {
        if (abilityActive)
        {
            abilityActive = false;
            new Timer(abilityCooldown, () => BenGaatDoodDoorHongerigeEgels());
            Collider[] temp = Physics.OverlapSphere(transform.position, 5);
            foreach (Collider col in temp) 
            { 
                if(col.TryGetComponent(out PlayerController player)) 
                {
                    player.LevelUp();
                }
            }
            var tempP = Instantiate(lvlParticle);
            tempP.transform.SetParent(transform);
            tempP.transform.position = new Vector3(transform.position.x, transform.position.y + particleYOfset, transform.position.z);
            Destroy(tempP, 1);
        }
    }

    private void BenGaatDoodDoorHongerigeEgels()
    {
        abilityActive = true;
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, levelRange);
    //}
}

