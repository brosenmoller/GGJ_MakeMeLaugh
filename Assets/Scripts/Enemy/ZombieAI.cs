using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] float viewRadius;
    private NavMeshAgent agent;
    private List<GameObject> hitPlayers = new List<GameObject>();

    private Collider[] targetsInViewRadius;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        { 
            hitPlayers.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hitPlayers.Remove(other.gameObject);
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        targetsInViewRadius = new Collider[100];
    }

    private void Update()
    {
        if(agent == null) 
        {
            return;
        }

        if (hitPlayers.Count > 0)
        {
            agent.SetDestination(transform.position);
            return;
        }
        var path = FindBestPath();
        agent.SetPath(path);
    }

    private NavMeshPath FindBestPath()
    {
        NavMeshPath shortestPath = null;
        float? shortestPathLength = null;

        int numberOfDetectedColliders = Physics.OverlapSphereNonAlloc(transform.position, viewRadius, targetsInViewRadius, targetMask);

        for (int i = 0; i < numberOfDetectedColliders; i++)
        {
            var newPath = new NavMeshPath();
            agent.CalculatePath(targetsInViewRadius[i].transform.position, newPath);
            float newPathLength = ExtraAIAtributes.CalcPathDistance(newPath);
            if (newPath.status == NavMeshPathStatus.PathComplete && (shortestPathLength == null || shortestPathLength > newPathLength)) 
            { 
                shortestPathLength = newPathLength;
                shortestPath = newPath;
            }
        }
        return shortestPath;
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
}
