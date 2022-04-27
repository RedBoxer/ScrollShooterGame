using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : SpawnWave
{
    public override void Spawn()
    {
        if (subwaveCount == 1)
        {
            Instantiate(enemies[0], new Vector3(20, 10, 0), enemies[0].transform.rotation);
            subwaveCount--;
        }

        if (FindObjectsOfType<Car>().Length == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
