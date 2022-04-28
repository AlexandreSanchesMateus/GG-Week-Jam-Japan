using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostYurei : MonoBehaviour
{
    [SerializeField] private BoxCollider2D m_collider2D;

    private void Awake()
    {
        m_collider2D.enabled = false;
        ActiveGhost(false);
    }

    public void ActiveGhost(bool active)
    {
        if (active)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            m_collider2D.enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            m_collider2D.enabled = false;
        }
    }

    public void Disappear()
    {
        //animation de disparition
        Destroy(gameObject);
    }
}
