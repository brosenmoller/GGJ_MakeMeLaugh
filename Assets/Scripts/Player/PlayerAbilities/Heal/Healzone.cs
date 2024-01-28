using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Healzone : MonoBehaviour
{
    float healRadius = 7;
    float healAmount = 1;
    bool endOfHeal = false;
    float timeBetweenHeal = 0.1f;
    string animeStringName = "Damage";
    public void startHeal(float healAmount,float totalHealTime) 
    {
        this.healAmount = healAmount;
        new Timer(totalHealTime, () => stopHeal());
        UpdateHeal();
    }
    
    void UpdateHeal() 
    {
        if (endOfHeal)
        {
            TryGetComponent(out Animator anime);
            anime.SetTrigger(animeStringName);
            return;
        }
        Collider[] temp = Physics.OverlapSphere(transform.position,healRadius);
        foreach (Collider c in temp) 
        { 
            if(TryGetComponent(out PlayerController player)) 
            {
                player.TakeDamage(-healAmount);
            }
        }

        new Timer(timeBetweenHeal,()=>UpdateHeal());
    }
    void stopHeal() 
    {
        endOfHeal = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, healRadius);
    }


}
