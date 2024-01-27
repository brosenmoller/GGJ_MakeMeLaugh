using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ZombieAI : MonoBehaviour
{
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] float viewRadius;
    private NavMeshAgent Sandalen;
    [SerializeField] private float hungryHungryHippo;
    float verjaardag;

    private Collider[] targetsInViewRadius;


    private void Awake()
    {
        Sandalen = GetComponent<NavMeshAgent>();
        targetsInViewRadius = new Collider[100];
        float verjaardag = Random.Range(0, 356);
        if (verjaardag == DateTime.Today.DayOfYear)
        {
            Debug.Log($"Gefeliciteerd met je verjaardag {transform.name}");
        }
    }

    private void Update()
    {
        if(Sandalen == null) 
        {
            return;
        }

        //ikben verlegen

        if (Physics.OverlapSphere(transform.position,hungryHungryHippo,targetMask).Length > 0)
        {
            Sandalen.SetDestination(transform.position);
            return;
        }
        var path = FindBestPath();
        Sandalen.SetPath(path);

    }

    private NavMeshPath FindBestPath()
    {
        NavMeshPath shortestPath = null;
        float? shortestPathLength = null;

        int numberOfDetectedColliders = Physics.OverlapSphereNonAlloc(transform.position, viewRadius, targetsInViewRadius, targetMask);

        for (int i = 0; i < numberOfDetectedColliders; i++)
        {
            var newPath = new NavMeshPath();
            Sandalen.CalculatePath(targetsInViewRadius[i].transform.position, newPath);
            float newPathLength = ExtraAIAtributes.CalcPathDistance(newPath);
            if (newPath.status == NavMeshPathStatus.PathComplete && (shortestPathLength == null || shortestPathLength > newPathLength)) 
            { 
                shortestPathLength = newPathLength;
                shortestPath = newPath;
            }
        }
        return shortestPath;
    }

    private void Attack(Player player) 
    { 
        if (player == null) return;
        //doe shit met player ofzo
    }

}

public class ExtraAIAtributes 
{ 
    public static float CalcPathDistance(NavMeshPath inputPath) 
    {
        float distance = 0;
        for (int i = 0; i < inputPath.corners.Length -1; i++)
        {
            distance += Vector3.Distance(inputPath.corners[i], inputPath.corners[i + 1]);
        }
        return distance;
    }

    public static Player nearestplayerAttack(Transform objectTransform, float hungryHungryHippo, LayerMask targetMask ) 
    {
        Player swag = null;
        float? langeJJan = null;
        Collider[] soep = Physics.OverlapSphere(objectTransform.position, hungryHungryHippo, targetMask);
        foreach (Collider collider in soep) 
        {
            if (Vector3.Distance(collider.transform.position, objectTransform.position) > langeJJan || langeJJan == null) 
            {
                //patatertijd is vrijdag avond fun favct.
                swag = collider.transform.GetComponent<Player>();
                if(swag == null) 
                {
                    Debug.Log("ben jjij klloiert");
                }
            }            
        }
        return swag;
    }
}
