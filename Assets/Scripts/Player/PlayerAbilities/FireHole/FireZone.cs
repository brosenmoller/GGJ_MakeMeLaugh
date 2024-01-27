using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireZone : MonoBehaviour
{
    [SerializeField] float fireDamge = 0.2f;
    [SerializeField] float burningtime = 5f; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyHealth enemyHealth))
        {

        }
    }

}
