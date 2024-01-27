using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningScrips : MonoBehaviour
{
    Material objMat;
    Color objColor;
    Color burncolor = Color.red;
    bool isBurning = true;
    float fireDamage = 0;
    PlayerController currectPlayer;
    float timeBetweenBurn;
    public void startBurning(float burnTime,float fireDamage,float amountOfBurns)
    {
        new Timer(burnTime+0.01f, () => stopBurning());
        this.fireDamage = fireDamage;
        timeBetweenBurn = burnTime/(amountOfBurns-1);
        BurnCheck();
    }
    private void Awake()
    {
        objMat = GetComponent<Material>();
        objColor = objMat.color;
    }
    public void stopBurning() 
    { 
        isBurning = false;
    }

    public void BurnCheck() 
    { 
        if (!isBurning) 
        {
            objMat.color = objColor;
            Destroy(this); return;
        }
        if (gameObject.TryGetComponent(out EnemyHealth enemyHealth)) 
        {
            enemyHealth.TakeDamage(fireDamage, currectPlayer, 0, Vector3.zero);
            objMat.color = burncolor;
        }
        new Timer(timeBetweenBurn, () => BurnCheck());
    }

    private void RevertColor() 
    { 
    
    }


}
