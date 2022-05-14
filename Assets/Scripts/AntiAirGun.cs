using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirGun : Enemy
{
    public GameObject gun;
    public GameObject healthBar;
    public Bullet Bullet;

    public int MaxHealth = 2;
    private int health;
    private GameObject Player;

    private float fraction;

    public bool invincible = false;
    void Start()
    {     
        Player = GameObject.Find("Player");
        health = MaxHealth;
        fraction = healthBar.transform.localScale.x / MaxHealth;
    }

    // Update is called once per frame
    public override void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(Player.transform.position - gun.transform.position,
                transform.TransformDirection(Vector3.forward));
        gun.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }

    public void MakeShot()
    {
       Instantiate(Bullet, gun.transform.position, gun.transform.rotation);  
    }

    new public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            if (!invincible)
            {
                health -= 1;

                healthBar.transform.localScale = new Vector2(healthBar.transform.localScale.x - fraction, healthBar.transform.localScale.y);

                if (health == 0)
                {
                    Destroy(this.gameObject);
                }
            }

            Destroy(other.gameObject);
        }
    }
}
