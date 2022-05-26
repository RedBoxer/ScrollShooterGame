using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStation : AntiAir
{
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = guns.Length;
        base.Start();
        bossName = "Station";
    }

    // Update is called once per frame
    void Update()
    {
        shotAllowed = FindObjectOfType<EnemyBullet>() == null;
        base.Update();
    }
}
