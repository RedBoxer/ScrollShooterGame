using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saucer : Car
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        bossName = "Saucer";
        Player = GameObject.Find("Player");
    }

    protected override void MakeShot()
    {
        if ((currentTime - startTime) >= checkTime)
        {
           
            Instantiate(Bullet, new Vector2(turret.transform.position.x - 1, turret.transform.position.y + 1), turret.transform.rotation);
            Instantiate(Bullet, turret.transform.position, turret.transform.rotation);
            Instantiate(Bullet, new Vector2(turret.transform.position.x + 1, turret.transform.position.y + 1), turret.transform.rotation);

            checkTime += 0.25f;
            rounds++;
        }

        if (rounds == 3)
        {
            rounds = 0;
            checkTime = 2.5f;
            startTime = Time.fixedTime;
        }
    }

}
