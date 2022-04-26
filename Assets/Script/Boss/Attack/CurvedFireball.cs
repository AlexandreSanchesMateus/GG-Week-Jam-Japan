using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedFireball : MonoBehaviour
{
    [SerializeField] Transform Player;

    public float speed;
    public float period;
    public float amplitude;

    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.y += Mathf.Sin(Time.time * 2 * Mathf.PI / period) * amplitude;
        transform.position = newPosition + Vector3.left * speed;
    }
}
