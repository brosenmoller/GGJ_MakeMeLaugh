using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowReset : MonoBehaviour
{
    public void InvokeSlowReset(NavMeshAgent agentP, float timeBeforeReset,float speed) 
    {
        new Timer(timeBeforeReset,() => UnSlow(agentP,speed));
    }

    void UnSlow(NavMeshAgent navMeshAgent, float speed) 
    {
        navMeshAgent.speed = speed;
        Destroy(this);
    }
}
