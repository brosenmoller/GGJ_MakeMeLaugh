using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningScrips : MonoBehaviour
{
    [SerializeField] Color burncolor = Color.red;
    bool isBurning = true;
    float fireDamage = 0;
    PlayerController currectPlayer;
    float timeBetweenBurn;
    public void startBurning(float burnTime, float fireDamage, float amountOfBurns, PlayerController playerController)
    {
        currectPlayer = playerController;
        new Timer(burnTime + 0.01f, () => stopBurning());
        this.fireDamage = fireDamage;
        timeBetweenBurn = burnTime / (amountOfBurns);
        BurnCheck();
    }
    public void stopBurning() 
    { 
        isBurning = false;
    }

    public void BurnCheck() 
    { 
        if (!isBurning) 
        {
            Destroy(this); return;
        }
        if (gameObject.TryGetComponent(out EnemyHealth enemyHealth) && currectPlayer != null) 
        {
            enemyHealth.TakeDamage(fireDamage, currectPlayer, 0, Vector3.zero,burncolor);
        }
        new Timer(timeBetweenBurn, () => BurnCheck());
    }
}
