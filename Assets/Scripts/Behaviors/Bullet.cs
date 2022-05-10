using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 100;

    public Case[] Cases;

    public bool piercing = false;

    protected bool isPlayer = true;

    public int caseId = 0;
    // Start is called before the first frame update
    protected void Start()
    {
        this.GetComponent<Rigidbody2D>().AddForce(this.transform.up * speed * 30);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void changeCase(int caseId)
    {
        foreach(Case Case in Cases)
        {
            if(Case.caseId == caseId)
            {
                Case.gameObject.SetActive(true);
                this.caseId = caseId;
            }
            else
            {
                Case.gameObject.SetActive(false);
            }
        }     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (!piercing && caseId != 3)
        {
            if (isPlayer && collision.tag == "Enemy")
            {
                Destroy(this.gameObject);
            }
            else if (!isPlayer && collision.tag == "Player")
            {
                Destroy(this.gameObject);
            }
        }

        if (caseId == 3)
        {
            if (isPlayer && collision.tag == "Enemy")
            {
                Cases[3].OnCollision();
            }
            else
            {
                Cases[0].OnCollision();
            }
        }
    }
}
