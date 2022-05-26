using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : AntiAir
{
    public GameObject WallSpawner;
    public GameObject Wall;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = guns.Length;
        base.Start();
        bossName = "Train";
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (currentTime - startTime >= checkTime - 0.3)
        {
            Instantiate(Wall, WallSpawner.transform.position, WallSpawner.transform.rotation);
        }
    }
}
