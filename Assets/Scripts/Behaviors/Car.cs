using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : BaseBoss
{
    public GameObject turret;
    public GameObject carBody;
    public GameObject[] stops;

    protected int rounds = 0;
    protected float checkTime = 2.5f;

    protected int target = 1;

    protected GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        bossName = "Car";
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    public override void Update()
    {
        currentTime = Time.fixedTime;

        if ((currentTime - startTime) >= 2)
        {
            Quaternion rotation = Quaternion.LookRotation(Player.transform.position - turret.transform.position, 
                transform.TransformDirection(Vector3.forward));
            turret.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

            MakeShot();
        }

        if ((Vector2)carBody.transform.position == (Vector2)stops[target].transform.position)
        {
            target++;
            if (target == 4)
            {
                target = 0;
            }
            //carBody.transform.Rotate(Vector3.forward, 90);
        }
        
        carBody.transform.position = Vector2.MoveTowards(carBody.transform.position, stops[target].transform.position, speed * Time.deltaTime);
    }

    protected virtual void MakeShot()
    {
        if ((currentTime - startTime) >= checkTime)
        {
            Instantiate(Bullet, turret.transform.position, Quaternion.Euler(new Vector3(0, 0, turret.transform.rotation.eulerAngles.z + 30)));
            Instantiate(Bullet, turret.transform.position, Quaternion.Euler(new Vector3(0, 0, turret.transform.rotation.eulerAngles.z + 15)));
            Instantiate(Bullet, turret.transform.position, turret.transform.rotation);
            Instantiate(Bullet, turret.transform.position, Quaternion.Euler(new Vector3(0, 0, turret.transform.rotation.eulerAngles.z - 15)));
            Instantiate(Bullet, turret.transform.position, Quaternion.Euler(new Vector3(0, 0, turret.transform.rotation.eulerAngles.z - 30)));
            checkTime += 0.5f;
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
