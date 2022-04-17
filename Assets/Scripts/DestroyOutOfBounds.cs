using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = 20f;
    private float lowerBound = -20f;
    private float leftBound = -50f;
    private float rightBound = 50f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > topBound || 
            transform.position.y < lowerBound ||
            transform.position.x < leftBound ||
            transform.position.x > rightBound)
        {
            Destroy(gameObject);
        }
    }
}
