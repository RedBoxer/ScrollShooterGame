using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWaveRain : SpawnWave
{
    public override void Spawn()
    {
        currentTime = Time.fixedTime;
        if ((currentTime - startTime) >= spawnDelay)
        {
            for (int i = 0; i < enemiesCount; i++)
            {
                Instantiate(enemies[0], GenerateRandomPos(), this.transform.rotation);
            }
            
            startTime = Time.fixedTime;
            subwaveCount--;
        }

        if (subwaveCount == 0)
        {
            Destroy(this.gameObject);
        }
    }

    Vector3 GenerateRandomPos()
    {
        return this.transform.position + Vector3.left * Random.Range(-5, 5);
    }
}
