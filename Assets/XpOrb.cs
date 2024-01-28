using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpOrb : MonoBehaviour
{
    [SerializeField] float XpPerorb = 0.1f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player)) 
        { 
            player.GiveXp(XpPerorb);
            Destroy(gameObject);
        }
    }

}
