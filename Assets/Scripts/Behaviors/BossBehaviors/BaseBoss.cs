using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseBoss : Enemy
{
    public GameObject HealthBar;
    public GameObject Bullet;

    protected float startTime;
    protected float currentTime;

    protected int health;
    protected int maxHealth = 10;

    public string bossName;
    // Start is called before the first frame update
    protected void Start()
    {
        health = maxHealth;
        startTime = Time.fixedTime;
        HealthBar = GameObject.FindGameObjectWithTag("BossHealth");
        HealthBar.GetComponent<Slider>().value = 1;
    }

    new public virtual void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            health -= 1;

            float percentOfLoss = (float)health / maxHealth;

            Debug.Log(percentOfLoss);

            HealthBar.GetComponent<Slider>().value = percentOfLoss;

            if (health == 0)
            {
                HealthBar.SetActive(false);
                Destroy(this.gameObject);
                FindObjectOfType<GameController>().AddScore(this.tag);
                MainManager.Instance.GetCurrentUser().confirmKill(bossName);
            }
        }
    }
}
