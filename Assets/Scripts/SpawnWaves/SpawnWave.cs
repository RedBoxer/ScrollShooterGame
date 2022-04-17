using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWave : MonoBehaviour
{
    public GameObject[] enemies;
    public int enemiesCount;
    public int subwaveCount;
    public float spawnDelay = 1f;
    public int[] allowedPoints = { 1, 1, 1, 1, 1, 1, 1 };  
    protected float startTime;
    protected float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.fixedTime;
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    public virtual void Spawn()
    {
        currentTime = Time.fixedTime;
        if ((currentTime - startTime) >= spawnDelay)
        {
            Instantiate(enemies[0], this.transform.position, this.transform.rotation);
            startTime = Time.fixedTime;
            subwaveCount--;
        }

        if (subwaveCount == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
