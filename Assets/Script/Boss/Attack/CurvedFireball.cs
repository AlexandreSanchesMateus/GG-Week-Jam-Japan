using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedFireball : MonoBehaviour
{
    public float speed;
    public float period;
    public float amplitude;

    private void Start()
    {
        Invoke("DestroyObject", 5f);
    }

    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.y += Mathf.Sin(Time.time * 2 * Mathf.PI / period) * amplitude;
        transform.position = newPosition - transform.right * speed * Time.deltaTime;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
