using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawnWave : SpawnWave
{
    // Start is called before the first frame update
    public override void Spawn()
    {
        if (subwaveCount == 1)
        {
            Instantiate(enemies[0], new Vector3(0, 0, 0), enemies[0].transform.rotation);
            subwaveCount--;
        }

        if (FindObjectsOfType<BaseBoss>().Length == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
