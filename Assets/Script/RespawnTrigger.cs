using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private Transform _RespawnPoint;
    private Collider2D _collider;

    private void Start()
    {
        _collider = gameObject.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponent<PlayerBase>().TakeDamage();
            other.transform.position = _RespawnPoint.position;
        }
        else
            Destroy(other.gameObject);
    }
}
