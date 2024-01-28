using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] GameObject Ass;
    [SerializeField] GameObject Mage;
    [SerializeField] GameObject Shield;
    [SerializeField] GameObject Bard;
    [SerializeField] GameObject[] List = new GameObject[] {};
    [SerializeField] GameObject UIElement;

    [SerializeField] float RespawnTimerInSec = 40;

    private bool someoneDied = false;
    private float timer;

    private void Awake()
    {
        timer = RespawnTimerInSec;
        List = new GameObject[4] {Ass,Mage,Shield,Bard};
    }
    private void Update()
    {
        if (!Ass.activeInHierarchy || !Mage.activeInHierarchy || !Bard.activeInHierarchy || !Shield.activeInHierarchy) 
        { 
            someoneDied = true;
            UIElement.SetActive(true);
        }

        if(timer < 0) 
        {
            foreach (var item in List) 
            {
                //Debug.Log(item.name);
                if (!item.activeInHierarchy) 
                {
                    //Debug.Log(item.name);
                    item.GetComponent<PlayerController>().ResertHealth();
                    item.SetActive(true);
                }
            }
            someoneDied = false;
            UIElement.SetActive(false);
        }

        if (someoneDied && !Input.anyKey) 
        { 
            timer -= Time.deltaTime;
        }
        else 
        {
            timer = RespawnTimerInSec;
        }

    }
}
