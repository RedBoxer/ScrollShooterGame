using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterSpawn : SpawnWave
{
    public override void Spawn()
    {
        if (subwaveCount == 1)
        {
            Instantiate(enemies[0], this.transform.position - new Vector3(10, 0, 0), enemies[0].transform.rotation);
            subwaveCount--;
        }

        if (FindObjectsOfType<Helicopter>().Length == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
   