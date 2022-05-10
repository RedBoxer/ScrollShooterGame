using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitCase : Case
{
    private GameObject Player;
    public Bullet caseless;
    private bool shot = false;
    private Vector3 prespawnPlayerPos;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        prespawnPlayerPos = Player.transform.position;
        caseId = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {

            if ((System.Math.Truncate(bullet.transform.position.y) == System.Math.Truncate(Player.transform.position.y + 10)) &&
                !shot)
            {
                Bullet b1 = Instantiate(caseless, bullet.transform.position, Quaternion.Euler(new Vector3(0, 0, bullet.transform.rotation.eulerAngles.z + 90)));
                Bullet b2 = Instantiate(caseless, bullet.transform.position, Quaternion.Euler(new Vector3(0, 0, bullet.transform.rotation.eulerAngles.z - 90)));
                b1.changeCase(0);
                b2.changeCase(0);
                shot = true;
            }
        }
        else
        { 
            if ((System.Math.Truncate(transform.position.y) == System.Math.Truncate(Player.transform.position.y)) && 
                !shot)
            {
                Instantiate(caseless, transform.position, Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + 90)));
                Instantiate(caseless, transform.position, Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z - 90)));
                shot = true;
            }
        }
    }
}
