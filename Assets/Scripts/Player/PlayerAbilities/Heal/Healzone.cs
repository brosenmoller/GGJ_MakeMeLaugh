using UnityEngine;

public class Healzone : MonoBehaviour
{
    float healRadius = 7;
    float healAmount = 1;
    float timeBetweenHeal = 0.1f;
    bool endOfHeal = false;
    string animeStringName = "Destroy";
    public void startHeal(float healAmount,float totalHealTime, float timeBetweenHeal) 
    {
        this.healAmount = healAmount;
        this.timeBetweenHeal = timeBetweenHeal;
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
            if(c.TryGetComponent(out PlayerController player)) 
            {
                player.TakeDamage(-healAmount);
                //Debug.Log(c.gameObject.name);
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
