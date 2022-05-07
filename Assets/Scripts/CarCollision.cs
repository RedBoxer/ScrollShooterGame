using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    public BaseBoss parrent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        parrent.OnTriggerEnter2D(collision);
    }
}
