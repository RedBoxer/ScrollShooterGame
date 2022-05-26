using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Car
{
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 20;

        base.Start();

        bossName = "Tank";
        Player = GameObject.Find("Player");
        checkTime = 2.75f;
    }

    protected override void MakeShot()
    {   
        if ((currentTime - startTime) >= checkTime)
        {
            Instantiate(Bullet, turret.transform.position, turret.transform.rotation);
            checkTime += 0.5f;
        }   
    }
}
