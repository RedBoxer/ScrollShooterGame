using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private MainManager.UserData currentUser;
    public Bullet[] bullets;

    private string currentBarrel = "Standart";
    private int currentBullet = 0;
    private int currentCase = 0;

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
        currentBullet = 0;
        foreach (KeyValuePair<string, bool> pair in currentUser.killedBosses)
        {
            if ((pair.Key == "Car" || pair.Key == "Hellicopter" || pair.Key == "Saucer") && pair.Value)
            {
                currentBarrel = pair.Key;
            }

            if ((pair.Key == "Tank" || pair.Key == "Jet" || pair.Key == "Train") && pair.Value)
            {
                switch (pair.Key)
                {
                    case "Tank":
                        currentBullet = 1;
                        break;
                    case "Jet":
                        currentBullet = 2;
                        break;
                    case "Train":
                        currentBullet = 3;
                        break;
                }
            }

            if ((pair.Key == "AntiAir") && pair.Value)
            {
                switch (pair.Key)
                {
                    case "AntiAir":
                        currentCase = 1;
                        break;
                }
            }
        }

        bullets[currentBullet].changeCase(currentCase);
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
                        Instantiate(bullets[currentBullet], this.transform.position, bullets[currentBullet].transform.rotation);
                        shotComplete = true;
                    }
                    break;
                case "Hellicopter":
                    Instantiate(bullets[currentBullet], new Vector2(currentPosition.x - (TwinOffset * side), currentPosition.y), bullets[currentBullet].transform.rotation);
                    side *= -1;
                    shotComplete = true;
                    break;
                case "Car":
                    if ((currentTime - startTime) >= shotDelay * 2)
                    {
                        Instantiate(bullets[currentBullet], transform.position, Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + 30)));
                        Instantiate(bullets[currentBullet], transform.position, Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + 15)));
                        Instantiate(bullets[currentBullet], transform.position, transform.rotation);
                        Instantiate(bullets[currentBullet], transform.position, Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z - 15)));
                        Instantiate(bullets[currentBullet], transform.position, Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z - 30)));
                        shotComplete = true;
                    }
                    break;
                case "Saucer":
                    if ((currentTime - startTime) >= shotDelay * 2)
                    {
                        Instantiate(bullets[currentBullet], new Vector2(transform.position.x - 1, transform.position.y - 1), transform.rotation);
                        Instantiate(bullets[currentBullet], transform.position, transform.rotation);
                        Instantiate(bullets[currentBullet], new Vector2(transform.position.x + 1, transform.position.y - 1), transform.rotation);

                        shotComplete = true;
                    }
                    break;
            }

            if (shotComplete)
            {
                startTime = currentTime;
            }
        }        
    }
}
