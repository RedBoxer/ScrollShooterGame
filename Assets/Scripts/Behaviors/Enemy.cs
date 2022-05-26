using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 100;

    // Update is called once per frame
    public virtual void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            FindObjectOfType<GameController>().AddScore(this.tag);
            Destroy(this.gameObject);
        }  
    }
}
