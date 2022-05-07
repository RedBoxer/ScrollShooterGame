using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    //must be in order from lowest left point to lowest right
    public SpawnPoint[] spawnPoints;
    public SpawnWave[] spawnWaves;
    public SpawnWave[] bossWaves;

    public GameObject BossHealth;

    public float spawnDelay = 6f;

    public bool DebugStop = false;

    //starts with 1 because first wave is spawned in Start()
    public int waveCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        SpawnFromRandomAllowedPoint(spawnWaves[Random.Range(0, spawnWaves.Length)]);
        BossHealth.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectsOfType<SpawnWave>().Length == 0 && !(FindObjectOfType<PlayerController>().isDead) && !DebugStop)
        {
            if (waveCount == 5)
            {
                SpawnFromRandomAllowedPoint(bossWaves[Random.Range(0, bossWaves.Length)]);
                BossHealth.SetActive(true);
                waveCount = 0;
            }
            else
            {
                waveCount++;
                SpawnFromRandomAllowedPoint(spawnWaves[Random.Range(0, spawnWaves.Length)]);
            }
        }
    }

    void SpawnFromRandomAllowedPoint(SpawnWave spawnWave)
    {
        List<SpawnPoint> allowedPoints = new List<SpawnPoint>();
        // Prepare spawnpoint set for wave
        for (int i = 0; i < spawnWave.allowedPoints.Length; i++)
        {
            if (spawnWave.allowedPoints[i] == 1)
            {
                allowedPoints.Add(spawnPoints[i]);
            }
        }

        // Choose random point from them
        SpawnPoint[] temp = allowedPoints.ToArray();
        temp[Random.Range(0, temp.Length)].SpawnWave(spawnWave);
    }
}
