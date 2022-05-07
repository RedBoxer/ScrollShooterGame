using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    public Bullet bullet;
    public bool isPlayer = false;
    public int caseId = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void insertBullet(Bullet inBullet)
    {      
        bullet = inBullet;
        if (bullet.tag == "PlayerBullet")
        {
            isPlayer = true;
        }
    }
}
