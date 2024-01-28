using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireZone : MonoBehaviour
{
    [SerializeField] float fireDamge = 0.2f;
    [SerializeField] float burningtime = 5f;
    [SerializeField] float amoundOfBurns = 5f;
    [SerializeField] string DestroyAnimationTrigger = "Destroy";
    [SerializeField] PlayerController playerController = new PlayerController();
    [SerializeField] GameObject fireParticle;
    [SerializeField] float YParticleOfset;


    public void StartFire(float circleDespawnTime,float burnTime,float fireDamage,float amoundOfBurns,PlayerController playerController) 
    {
        this.amoundOfBurns = amoundOfBurns;
        burningtime = burnTime;
        fireDamge = fireDamage;
        this.playerController = playerController;
        new Timer(circleDespawnTime, () => Despawn());
    }
    
    private void Despawn() 
    { 
        if(TryGetComponent(out Animator animator)) 
        {
            animator.SetTrigger(DestroyAnimationTrigger);
        }
        else 
        {
            Debug.LogWarning($"animator not assigned on {gameObject.name}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyHealth enemyHealth) && !TryGetComponent(out BurningScrips burn))
        {
            var temp = other.gameObject.AddComponent<BurningScrips>();
            temp.startBurning(burningtime, fireDamge, amoundOfBurns, playerController);
            var fireTemp = Instantiate(fireParticle);
            fireTemp.transform.parent = temp.transform;
            fireTemp.transform.position = new Vector3(temp.transform.position.x,temp.transform.position.y + YParticleOfset,temp.transform.position.z);
            Destroy(fireTemp,burningtime);
        }
    }

}
