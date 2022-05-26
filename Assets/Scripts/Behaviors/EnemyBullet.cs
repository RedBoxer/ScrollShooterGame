using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private void Start()
    {
        isPlayer = false;
        this.GetComponent<Rigidbody2D>().AddForce(this.transform.up * speed * -1 * 30);
    }
}
