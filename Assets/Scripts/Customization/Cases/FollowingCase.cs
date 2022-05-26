using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCase : Case
{
    private GameObject target;
    private Rigidbody2D bulletRb;
    // Start is called before the first frame update
    void Start()
    {
        caseId = 2;
        bulletRb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (isPlayer)
        {
            target = FindObjectOfType<Enemy>().gameObject;
        }
        else
        {
            target = GameObject.Find("Player");
        }     
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 lookDirection = (target.transform.position - bullet.transform.position).normalized;
            bulletRb.AddForce(lookDirection * bullet.speed / 2);
        }
    }
}
