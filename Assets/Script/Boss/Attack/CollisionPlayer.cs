using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlayer : MonoBehaviour
{
    public int _damage = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Collision détecté");
            }
        }
    }
}
