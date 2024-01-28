using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StunZone : MonoBehaviour
{
    [SerializeField] float stunTime = 3f;
    [SerializeField] float stunDamage = 3f;
    [SerializeField] PlayerController player;
    [SerializeField] SphereCollider sphereCollider;

    public void Instanciate(float stunTime,float stunDamage,PlayerController player) 
    { 
        this.stunTime = stunTime;
        this.stunDamage = stunDamage;
        this.player = player;
        sphereCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out NavMeshAgent nev))
        {
            if (nev.gameObject.TryGetComponent(out SlowReset slowReset))
            {
                nev.speed = nev.speed * 0;
                if (nev.gameObject.TryGetComponent(out EnemyHealth healt))
                {
                    healt.TakeDamage(stunDamage, player, 4, (transform.position - other.transform.position).normalized, Color.white);
                }
                return;
            }
            var temp = nev.gameObject.AddComponent<SlowReset>();
            float tempSpeed = nev.speed;
            temp.InvokeSlowReset(nev, stunTime, tempSpeed);
            nev.speed = nev.speed * 0;

            if(nev.gameObject.TryGetComponent(out EnemyHealth health)) 
            {
                health.TakeDamage(stunDamage,player,10,(transform.position - other.transform.position).normalized,Color.white);
            }
        }
    }
}
