using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.transform.CompareTag("Enemie"))
            {
                Debug.Log("Enemie Touché");
                BossBase.instance.TakeDamage(5);
            }
            else if (collision.transform.CompareTag("Ground"))
            {
                Debug.Log("Environement Touché");
            }

            Destroy(gameObject);
        }
    }
}
