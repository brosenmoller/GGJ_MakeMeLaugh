using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoedeCode : MonoBehaviour
{
    [SerializeField] GameObject Zombie;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(90,0 ,0 - Zombie.transform.rotation.eulerAngles.z - 180));
    }
}
