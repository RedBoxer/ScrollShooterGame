using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetCase : Case
{
    private GameObject target;
    private Rigidbody2D bulletRb;
    private int hits = 0;
    private bool stopped = false;

    // Start is called before the first frame update
    void Start()
    {
        caseId = 3;
        bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (isPlayer)
        {
            target = null;
        }
        else
        {
            SetNewTarget();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 lookDirection = (target.transform.position - bullet.transform.position).normalized;
            bulletRb.AddForce(lookDirection * bullet.speed);
            stopped = false;
        }
        else if (stopped)
        {
            bulletRb.AddForce(bullet.transform.up * bullet.speed * 30);
        }
    }

    public override void OnCollision()
    {
        hits++;

        if (isPlayer)
        {
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            stopped = true;

            if (hits <= 3)
            {               
                target = FindObjectOfType<Enemy>().gameObject;
            } 
            else if (!bullet.piercing)
            {
                Destroy(bullet.gameObject);
            }
            else
            {
                target = null;
            }

            if (!target)
            {
                bulletRb.AddForce(bullet.transform.up * bullet.speed * 30);
            }
        }
        else
        {
            if (target != null)
            {
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }

            SetNewTarget();

            if (hits == 6)
            {          
                Destroy(target);
                Destroy(bullet.gameObject);
            }           
        }      
    }

    private void SetNewTarget()
    {
        GameObject oldTarget = target;
        AntiAirGun[] targets = FindObjectsOfType<AntiAirGun>();

        if (targets.Length == 1)
        {
            target = FindObjectOfType<AntiAirGun>().gameObject;
            Destroy(target);
            target = GameObject.Find("Player");
            return;
        }
        if (targets.Length > 2)
        {
            while (target == oldTarget)
            {
                AntiAirGun rndGun = targets[(int)Random.Range(0, targets.Length - 1)];

                target = rndGun.gameObject;
            }
        }
        else
        { 
            int i = 0;
            while (target == oldTarget && i < targets.Length)
            {
                AntiAirGun rndGun = targets[i];

                target = rndGun.gameObject;

                i++;
            }

            if (target == oldTarget)
            {
                target = null;
            }
        }
    }
}
