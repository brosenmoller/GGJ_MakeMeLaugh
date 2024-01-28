using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowZone : MonoBehaviour
{
    [SerializeField] float slowfactor = 0.7f; 
    [SerializeField] float slowtime = 3f;
    [SerializeField] GameObject stunParticle;
    [SerializeField] float particleYOfset;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out NavMeshAgent nev))
        {
            if (nev.gameObject.TryGetComponent(out SlowReset slowReset)) 
            {
                nev.speed = nev.speed * slowfactor;
                return;
            }
            else 
            {
                var temp = nev.gameObject.AddComponent<SlowReset>();
                float tempSpeed = nev.speed;
                temp.InvokeSlowReset(nev,slowtime,tempSpeed);
                nev.speed = nev.speed * slowfactor;
                var tempP = Instantiate(stunParticle);
                tempP.transform.SetParent(nev.transform);
                tempP.transform.position = new Vector3(nev.transform.position.x, nev.transform.position.y + particleYOfset, nev.transform.position.z);
                Destroy(tempP, slowtime);
            }
        }
    }

    public void SetSlowVar(float newSlowFactor,float newSlowTime) 
    { 
        slowfactor = newSlowFactor;
        slowtime = newSlowTime;
    }
}
