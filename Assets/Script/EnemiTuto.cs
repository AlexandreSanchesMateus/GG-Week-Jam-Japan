using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiTuto : MonoBehaviour
{
    [SerializeField] private int _life = 2;
    [SerializeField] private float force;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Weapon"))
        {
            _life--;
            if(_life <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                Vector2 Direction = transform.position - collision.transform.position;
                gameObject.GetComponent<Rigidbody2D>().AddForce(Direction.normalized * force);
            }
        }
    }
}
