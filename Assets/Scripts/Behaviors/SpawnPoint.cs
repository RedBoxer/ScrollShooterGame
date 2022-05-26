using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public void SpawnWave(SpawnWave sw)
    {
        Instantiate(sw, this.transform.position, this.transform.rotation);
    }
}
