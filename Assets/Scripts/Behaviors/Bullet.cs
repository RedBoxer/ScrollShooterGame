using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 100;

    public Case[] Cases;

    public bool piercing = false;
    // Start is called before the first frame update
    void Start()
    {
        if (Cases[0] != null)
        {
            Cases[0].insertBullet(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    public void changeCase(int caseId)
    {
        foreach(Case Case in Cases)
        {
            if(Case.caseId == caseId)
            {
                Case.gameObject.SetActive(true);
            }
            else
            {
                Case.gameObject.SetActive(false);
            }
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!piercing && collision.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
