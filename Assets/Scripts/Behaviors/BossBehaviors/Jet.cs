using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : Helicopter
{
    private void Start()
    {
        base.Start();
        bossName = "Jet";
    }
    protected override void MakeShot()
    {
        if ((currentTime - startTime) >= 0.15)
        {
            if (left)
            {
                Instantiate(Bullet, new Vector2(TurretLeft.transform.position.x - 0.5f, TurretLeft.transform.position.y), TurretLeft.transform.rotation);
            }
            else
            {
                Instantiate(Bullet, new Vector2(TurretLeft.transform.position.x + 0.5f, TurretLeft.transform.position.y), TurretLeft.transform.rotation);
            }

            left = !left;

            startTime = Time.fixedTime;
        }
    }
}
