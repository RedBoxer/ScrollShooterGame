using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float verticalInput;
    private float horizontalInput;
    public float speed = 5;
    public GameObject bullet;

    private int maxHealth = 10; 
    private int healthPoints;
    private float healthBarInitialSize;
    public GameObject HealthBar;

    private float startTime;
    private float currentTime;
    private float shotDelay = 0.5f;

    private int horBound = 31;
    private int verBound = 16;

    Camera camera;
    Vector3 touchPos;

    // Start is called before the first frame update

    void Start()
    {
        startTime = Time.fixedTime;
        healthPoints = maxHealth;
        healthBarInitialSize = HealthBar.transform.localScale.x;
        Debug.Log(healthBarInitialSize);
        camera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.fixedTime;

        /////////////////////////////////
        /// if space pressed spawn bullet every shotDelay seconds
#if UNITY_ANDROID
        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                touchPos = touch.deltaPosition;
                transform.position = new Vector3(transform.position.x + touchPos.x * 0.04f, transform.position.y + touchPos.y * 0.04f, transform.position.z);
            }

        if(
#else
        if (Input.GetKey(KeyCode.Space) &&
#endif
         (currentTime - startTime) >= shotDelay)
        {
                Instantiate(bullet, this.transform.position, bullet.transform.rotation);

                startTime = currentTime;
        }
#if UNITY_ANDROID
        }
#endif
    }

    void FixedUpdate()
    {
#if !UNITY_ANDROID
        ////////////////////////////////
        //make player move left or right on pressing a/d or left right arrow
        if (transform.position.x <= horBound && transform.position.x >= -horBound)
        {
            

            
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector2.right * Time.deltaTime * horizontalInput * speed);

        }
        else
        {
            int retPos = horBound;
            if (transform.position.x < 0)
            {
                retPos *= -1;
            }
            transform.position = new Vector2(retPos, this.transform.position.y);
        }
        ////////////////////////////////

        ////////////////////////////////
        //make player move up or down on pressing w/s or up down arrow
        if (transform.position.y <= verBound && transform.position.y >= -verBound)
        {
            verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector2.up * Time.deltaTime * verticalInput * speed);
        }
        else
        {
            int retPos = verBound;
            if (transform.position.y < 0)
            {
                retPos *= -1;
            }
            transform.position = new Vector2(this.transform.position.x, retPos);
        }
        ////////////////////////////////
        ///
#endif
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }

        if (healthPoints == 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void TakeDamage(int damage)
    {
        healthPoints -= damage;

        float percentOfLoss = (float)healthPoints / maxHealth;

        HealthBar.GetComponent<Slider>().value = percentOfLoss; 
    }
}
