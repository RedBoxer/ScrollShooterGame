using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntiAir : BaseBoss
{
    public AntiAirGun[] guns;

    protected float checkTime = 2;
    protected int currentGun = 0;

    public bool shotAllowed = true;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = guns.Length;
        base.Start();
        bossName = "AntiAir";
    }

    // Update is called once per frame
    public override void Update()
    {
        currentTime = Time.fixedTime;

        health = NotNullGuns();

        float percentOfLoss = (float)health / maxHealth;

        HealthBar.GetComponent<Slider>().value = percentOfLoss;

        if (health == 0)
        {
            HealthBar.SetActive(false);
            Destroy(this.gameObject);
            FindObjectOfType<GameController>().AddScore(this.tag);
            MainManager.Instance.GetCurrentUser().confirmKill(bossName);
        }

        if (currentTime - startTime >= checkTime)
        {
            while (guns[currentGun] == null && currentGun < guns.Length)
            {
                if (guns[currentGun] == null && currentGun == guns.Length - 1)
                {
                    currentGun = 0;
                }
                else
                {
                    currentGun++;
                }
            }

            if (shotAllowed)
            {
                guns[currentGun].MakeShot();
                currentGun++;
            }

            if (currentGun >= guns.Length)
            {
                currentGun = 0;
            }

            startTime = Time.fixedTime;
        }
    }

    protected int NotNullGuns()
    {
        int result = 0;
        for (int i = 0; i < guns.Length; i++)
        {
            if (guns[i] != null)
            {
                result++;
            }
        }
        return result;
    }
}
