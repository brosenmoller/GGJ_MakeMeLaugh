using TMPro;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombie;
    public float zombieFallInterval = 4;
    public float reduceIntervalAmount = 2;
    public int reduceIntervalWhen = 3;
    public Vector2 IntervalMultiplier = new Vector2(0.3f, 1.7f);
    int time = 0;

    void Start()
    {
        SpawnZombie();
    }

    void SpawnZombie()
    {
        Vector3 spawnLocation = new(Random.Range(-25.0f, 25.0f), transform.position.y, Random.Range(-25.0f, 25.0f));
        Instantiate(zombie, spawnLocation, transform.rotation);

        time += 1;

        if (time % reduceIntervalWhen == 0 && zombieFallInterval > 0)
        {
            zombieFallInterval -= reduceIntervalAmount;
            reduceIntervalWhen += reduceIntervalWhen / 2;
        }
        Invoke(nameof(SpawnZombie), zombieFallInterval * Random.Range(IntervalMultiplier.x, IntervalMultiplier.y));
    }
}
