using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Helicopter : BaseBoss
{ 
    private float mainLength = 3f;

    private int step = 0;
    private int subStep = 0;
    public float length = 1;

    public GameObject TurretLeft, TurretRight;

    private bool left = true;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        bossName = "Hellicopter";
    }

    // Update is called once per frame
    public override void Update()
    {
        currentTime = Time.fixedTime;

        if ((currentTime - startTime) >= 0.3)
        {
            if (left)
            {
                Instantiate(Bullet, TurretLeft.transform.position, TurretLeft.transform.rotation);
            }
            else
            {
                Instantiate(Bullet, TurretRight.transform.position, TurretRight.transform.rotation);
            }

            left = !left;

            startTime = Time.fixedTime;
        }

        Vector2 currentMovement = new Vector2(0, 0);
        switch (step)
        {
            case 9:
            case 0:
                currentMovement = new Vector2(-1, -1) / 2; //bacward down
                break;
            case 8:
            case 1:
                currentMovement = new Vector2(0, -1); //down
                break;
            case 7:
            case 2:
                currentMovement = new Vector2(1, -1) / 2; //forward down
                break;
            case 6:
            case 3:
                currentMovement = new Vector2(1, 0); //forward
                break;
            case 5:
            case 4:
                currentMovement = new Vector2(1, 1); //forward up
                break;
            case 12:
            case 11:
                currentMovement = new Vector2(-1, 1); //backward up
                break;
            case 13:
            case 10:
                currentMovement = new Vector2(-1, 0); //backward
                break;

        }

        currentMovement *= mainLength * Time.fixedDeltaTime;

        transform.Translate(currentMovement);
        int maxSubSteps = 50;
        subStep++;
        if (step == 3 ||
            step == 6 ||
            step == 13 ||
            step == 10)
        {
            maxSubSteps = 300;
        }
        else
        {
            maxSubSteps = 75;
        }

        if (subStep == maxSubSteps)
        {
            step++;
            subStep = 0;
        }

        if (step == 14)
        {
            step = 0;
        }
    }

}
