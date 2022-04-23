using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private MainManager.UserData currentUser;
    public Bullet bullet;

    private string currentBarrel = "Standart";

    private float startTime;
    private float currentTime;
    private float shotDelay = 0.5f;

    private float TwinOffset = 0.5f;
    private int side = 1;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.fixedTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.fixedTime;
    }

    public void AssembleWeapon()
    {
        currentUser = MainManager.Instance.GetCurrentUser();
        currentBarrel = "Standart";
        foreach(KeyValuePair<string, bool> pair in currentUser.killedBosses)
        {
            if (pair.Value)
            {
                currentBarrel = pair.Key;
            }
        }
    }

    public void MakeShot()
    {
        Vector3 currentPosition = this.transform.position;
        bool shotComplete = false;
        if ((currentTime - startTime) >= shotDelay/2)
        {
            switch (currentBarrel)
            {
                case "Standart":
                    if ((currentTime - startTime) >= shotDelay)
                    {
                        Instantiate(bullet, this.transform.position, bullet.transform.rotation);
                        shotComplete = true;
                    }
                    break;
                case "Hellicopter":
                    Instantiate(bullet, new Vector2(currentPosition.x - (TwinOffset * side), currentPosition.y) , bullet.transform.rotation);
                    side *= -1;
                    shotComplete = true;
                    break;
            }

            if (shotComplete)
            {
                startTime = currentTime;
            }
        }        
    }
}
