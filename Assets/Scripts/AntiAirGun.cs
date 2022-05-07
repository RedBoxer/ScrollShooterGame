using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirGun : MonoBehaviour
{
    public GameObject gun;
    public GameObject healthBar;
    public Bullet Bullet;

    private int health = 2;
    private GameObject Player;
    void Start()
    {     
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(Player.transform.position - gun.transform.position,
                transform.TransformDirection(Vector3.forward));
        gun.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }

    public void MakeShot()
    {
        Instantiate(Bullet, gun.transform.position, gun.transform.rotation);
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            health -= 1;

            healthBar.transform.localScale = new Vector2(healthBar.transform.localScale.x / 2, healthBar.transform.localScale.y);

            if (health == 0)
            {
                Destroy(this.gameObject);
            }

            Destroy(other.gameObject);
        }
    }
}
